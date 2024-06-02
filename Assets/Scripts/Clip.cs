using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip : MonoBehaviour
{
    public AudioPlayEvent audioClip;
    
    private void OnEnable()
    {
        EventManager.Instance.InvokeEvent(audioClip);
    }
}
