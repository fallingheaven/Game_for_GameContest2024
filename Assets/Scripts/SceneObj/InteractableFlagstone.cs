using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableFlagstone : MonoBehaviour,IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        if (interactor.CurrentElement == Element.Water)
        {
            Debug.Log("清洗被尘埃覆盖的石板。");
            Debug.Log("显示游戏背景与元素特性。");
        }
    }
}
