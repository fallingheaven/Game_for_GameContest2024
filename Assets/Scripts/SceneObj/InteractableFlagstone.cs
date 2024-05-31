using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableFlagstone : MonoBehaviour,IInteract
{
    private bool _IsCleaned = false;
    public void Interact(CharacterBehavior interactor)
    {
        if(_IsCleaned == false)
        {
            if (interactor.CurrentElement == Element.Water)
            {
                Debug.Log("清洗被尘埃覆盖的石板。");
                Debug.Log("显示游戏背景与元素特性。");
                _IsCleaned = true;
                interactor.ResetElement();
            }
            else
            {
                Debug.Log("石板被尘埃覆盖了，也许 *水* 元素进行清洗");
            }
        }
        else
        {
            Debug.Log("显示游戏背景与元素特性。");
        }
    }
}
