using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utility.Interface;

public class AfterSceneLoadEvent : IEventMessage
{
    public AsyncOperationHandle<SceneInstance> operation;

    public AfterSceneLoadEvent(AsyncOperationHandle<SceneInstance> asyncOperation)
    {
        operation = asyncOperation;
    }
}

public class BeforeSceneLoadEvent : IEventMessage
{
    public AsyncOperationHandle<SceneInstance> operation;
}