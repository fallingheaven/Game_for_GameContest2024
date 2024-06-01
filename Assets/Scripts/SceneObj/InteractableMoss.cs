using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableMoss : MonoBehaviour, IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        Debug.Log("absorbed wood element");
        interactor.AbsorbElement(Element.Wood);
        Debug.Log("Moss disappeared");
    }
}
