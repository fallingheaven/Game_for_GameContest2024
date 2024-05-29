using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableLadder : MonoBehaviour, IInteract
{
    private bool _Fixed = false; // _Fixed = false 表示梯子损坏
    public void Interact(CharacterBehavior interactor)
    {
        if (_Fixed == false) // 如果梯子损坏
        {
            if (interactor.CurrentElement == Element.Wood)
            {
                Debug.Log("修复梯子");
                _Fixed = true;
            }
            else
            {
                Debug.Log("梯子已损坏，需要 *木* 元素来修复");
            }
        }
        else // 梯子完整则进入下个关卡
        {
            Debug.Log("进入下一关");
        }
    }
}
