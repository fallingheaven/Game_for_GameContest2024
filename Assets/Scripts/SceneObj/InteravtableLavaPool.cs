using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteravtableLavaPool : MonoBehaviour,IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        Debug.Log("吸收火元素");
        interactor.AbsorbElement(Element.Fire);
    }
}
