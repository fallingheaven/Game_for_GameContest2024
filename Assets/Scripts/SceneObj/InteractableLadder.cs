using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableLadder : MonoBehaviour, IInteract
{
    
    private bool _Fixed = false; // _Fixed = false
    public void Interact(CharacterBehavior interactor)
    {
        if (_Fixed == false)
        {
            if (interactor.CurrentElement == Element.Wood)
            {
                Debug.Log("fixed the ladder successfully");
                _Fixed = true;
            }
            else
            {
                Debug.Log("maybe you need *WOOD* element to fix the ladder");
            }
        }
        else 
        {
            Debug.Log("go to the next level");
        }
    }
}
