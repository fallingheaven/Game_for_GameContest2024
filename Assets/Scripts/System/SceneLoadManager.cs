using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Utility;
using Utility.CustomClass;
using Utility.Interface;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    private Dictionary<AssetReference, bool> _isLoaded = new Dictionary<AssetReference, bool>();
    
    private void OnEnable()
    {
        EventManager.Instance.SubscribeEvent<AfterSceneLoadEvent>(OnSceneLoaded);
    }
    
    private void OnDisable()
    {
        EventManager.Instance.UnsubscribeEvent<AfterSceneLoadEvent>(OnSceneLoaded);
    }

    public void UnloadScene(GameSceneSO sceneAsset)
    {
        if (!_isLoaded.ContainsKey(sceneAsset.targetScene))
        {
            Debug.LogWarning($"{sceneAsset.targetScene} 未被加载，卸载失败");
            return;
        }
        
        _isLoaded.Remove(sceneAsset.targetScene);
        
        sceneAsset.targetScene.UnLoadScene();
        Debug.Log($"{sceneAsset.targetScene}被卸载");
    }

    /// <summary>
    /// 异步加载方法
    /// </summary>
    /// <param name="sceneAsset">场景资源</param>
    /// <param name="forceLoad">是否强制加载（不论是否已经加载）</param>
    /// <returns></returns>
    public IEnumerator LoadSceneAsync(GameSceneSO sceneAsset, bool forceLoad = false)
    {
        if (sceneAsset.type == SceneType.Level)
        {
            LevelManager.Instance.currentLevel = sceneAsset;
        }
        
        UnloadScene(GameManager.Instance.currentGameScene);
        
        if (_isLoaded.ContainsKey(sceneAsset.targetScene))
        {
            if (forceLoad)
            {
                yield return sceneAsset.targetScene.UnLoadScene().IsDone;
            }
            else
            {
                Debug.LogWarning($"{sceneAsset.targetScene} 已经被加载");
                yield break;
            }
        }

        _isLoaded[sceneAsset.targetScene] = true;
        
        var operation = sceneAsset.targetScene.LoadSceneAsync(LoadSceneMode.Additive, false);
        
        while (!operation.IsDone)
        {
            var value = operation.PercentComplete;
            // TODO: 加载条value=场景加载value
            
            yield return null;
        }

        var onSceneLoadedMessage = new AfterSceneLoadEvent(operation);
        EventManager.Instance.InvokeEvent<AfterSceneLoadEvent>(onSceneLoadedMessage);

        if (sceneAsset.existPlayer)
        {
            GameManager.Instance.playerCharacter.transform.position = sceneAsset.initPosition;
            GameManager.Instance.playerCharacter.SetActive(true);
        }
    }

    private void OnSceneLoaded(IEventMessage message)
    {
        if (message is AfterSceneLoadEvent msg)
        {
            msg.operation.Result.ActivateAsync();
        }
        
    }
}
