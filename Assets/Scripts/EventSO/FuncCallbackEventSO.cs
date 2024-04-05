using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/FuncCallback")]
public class FuncCallbackEventSO<T> : ScriptableObject
{
    public UnityAction<Func<T>> onEventRaised;

    public void RaiseEvent(Func<T> callback)
    {
        onEventRaised?.Invoke(callback);
    }
}
