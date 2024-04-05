using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/FuncCallback")]
public class FuncCallbackEventSO : ScriptableObject
{
    public UnityAction<Func<object>> onEventRaised;

    public void RaiseEvent(Func<object> callback)
    {
        onEventRaised?.Invoke(callback);
    }
}
