using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableBox : MonoBehaviour,IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        if (interactor.CurrentElement == Element.Wind)
        {
            Debug.Log("推动木箱。"); 
        }

    }
}
