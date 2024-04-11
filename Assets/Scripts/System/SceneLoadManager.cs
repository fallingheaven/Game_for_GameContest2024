using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.CustomClass;
using Utility.Interface;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
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
        sceneAsset.targetScene.UnLoadScene();
    }

    public IEnumerator LoadSceneAsync(GameSceneSO sceneAsset)
    {
        var operation = sceneAsset.targetScene.LoadSceneAsync(LoadSceneMode.Additive, false);
        
        while (!operation.IsDone)
        {
            var value = operation.PercentComplete;
            // TODO: 加载条value=场景加载value
            
            yield return null;
        }

        var onSceneLoadedMessage = new AfterSceneLoadEvent(operation);
        EventManager.Instance.InvokeEvent<AfterSceneLoadEvent>(onSceneLoadedMessage);
    }

    private void OnSceneLoaded(IEventMessage message)
    {
        if (message is AfterSceneLoadEvent msg)
        {
            msg.operation.Result.ActivateAsync();
        }
        
    }
}
