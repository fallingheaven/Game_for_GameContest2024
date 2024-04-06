using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Utility.CustomClass;

public class SceneLoadManager : Singleton<SceneLoadManager>
{

    public void UnloadScene(GameSceneSO sceneAsset)
    {
        sceneAsset.targetScene.UnLoadScene();
    }

    public IEnumerator LoadSceneAsync(GameSceneSO sceneAsset)
    {
        Debug.Log("1");
        var operation = sceneAsset.targetScene.LoadSceneAsync(LoadSceneMode.Additive, false);
        Debug.Log("2");
        while (!operation.IsDone)
        {
            var value = operation.PercentComplete;
            // TODO: 加载条value=场景加载value
            
            yield return null;
        }
        Debug.Log("3");
        StartCoroutine(OnSceneLoaded(operation));
    }

    private IEnumerator OnSceneLoaded(AsyncOperationHandle<SceneInstance> operation)
    {
        yield return operation.Result.ActivateAsync();
    }
}
