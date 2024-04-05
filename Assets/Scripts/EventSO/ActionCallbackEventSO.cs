using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ActionCallback")]
public class ActionCallbackEventSO : ScriptableObject
{
    public UnityAction<Action<object>> onEventRaised;

    public void RaiseEvent(Action<object> callback)
    {
        onEventRaised?.Invoke(callback);
    }
}
