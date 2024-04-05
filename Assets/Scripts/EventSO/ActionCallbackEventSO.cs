using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ActionCallback")]
public class ActionCallbackEventSO<T> : ScriptableObject
{
    public UnityAction<Action<T>> onEventRaised;

    public void RaiseEvent(Action<T> callback)
    {
        onEventRaised?.Invoke(callback);
    }
}
