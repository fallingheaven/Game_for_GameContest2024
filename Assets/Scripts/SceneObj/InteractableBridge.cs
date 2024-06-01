using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableBridge : MonoBehaviour, IInteract
{
    private bool _Fixed = false; // _Fixed = false
    public void Interact(CharacterBehavior interactor)
    {
        if (_Fixed == false)
        {
            if (interactor.CurrentElement == Element.Soil)
            {
                Debug.Log("fixed the bridge successfully");
                _Fixed = true;
            }
            else
            {
                Debug.Log("maybe you need *SOIL* element to fix the bridge");
            }
        }
        else 
        {
            Debug.Log("pass the bridge");
        }
    }
}
