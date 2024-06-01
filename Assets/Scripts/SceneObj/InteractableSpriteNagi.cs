using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableSpriteNagi : MonoBehaviour, IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        interactor.ResetElement();
        Debug.Log("You've got the permanent Wind element!");
    }
}
