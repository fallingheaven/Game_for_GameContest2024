using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableBridge : MonoBehaviour, IInteract
{
    private bool _Fixed = false; // _Fixed = false 表示桥梁损坏
    public void Interact(CharacterBehavior interactor)
    {
        if (_Fixed == false) // 如果桥梁损坏
        {
            if (interactor.CurrentElement == Element.Soil)
            {
                Debug.Log("修复桥梁");
                _Fixed = true;
            }
            else
            {
                Debug.Log("桥梁损坏，需要 *土* 元素来修复");
            }
        }
        else // 桥梁已修复
        {
            Debug.Log("可通过桥梁");
        }
    }
}
