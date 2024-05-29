using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableRubble : MonoBehaviour, IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        Debug.Log("吸收土元素");
        interactor.AbsorbElement(Element.Soil);
        Debug.Log("碎石堆消失");
    }
}
