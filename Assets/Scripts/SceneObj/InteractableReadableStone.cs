using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Interface;

public class InteractableReadableStone : MonoBehaviour, IInteract
{
    public string[] strings;
    public void Interact(CharacterBehavior interactor)
    {
        DialogueManager.instance.ShowDialogue(strings);
    }
}
