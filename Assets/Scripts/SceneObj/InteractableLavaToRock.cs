using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableLavaToRock : MonoBehaviour, IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        if (interactor.CurrentElement == Element.Rock) 
        {
            Debug.Log("generate rock on lava");
            interactor.ResetElement();
        }
    }
}
