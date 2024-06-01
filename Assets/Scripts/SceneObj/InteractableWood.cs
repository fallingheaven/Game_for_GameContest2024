using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableWood : MonoBehaviour, IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        if (interactor.AbsorbElement(Element.Wood))
        {
            Debug.Log("absorbed wood element");
        }
    }
}
