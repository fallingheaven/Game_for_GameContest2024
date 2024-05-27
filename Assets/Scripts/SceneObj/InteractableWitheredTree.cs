using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableWitheredTree : MonoBehaviour, IInteract
{
    // use Element to represent the withered tree's current state
    private Element nowElement = Element.Wood; // default element
    public void Interact(CharacterBehavior interactor)
    {
        switch (nowElement) 
        {
            case Element.Wood:
                if (interactor.CurrentElement == Element.Wetland)
                {
                    nowElement = interactor.CurrentElement;
                    Debug.Log("Transform to Wetland");
                    Debug.Log("Change the Icon");
                    interactor.ResetElement();
                }
                break;
            case Element.Wetland:
                if(interactor.CurrentElement == Element.Ember)
                {
                    nowElement = interactor.CurrentElement;
                    Debug.Log("Transform to Ember");
                    interactor.ResetElement();
                }
                break;
            case Element.Ember:
                if(interactor.CurrentElement == Element.Wind)
                {
                    Debug.Log("Transform to high tree");
                    Debug.Log("Change the Icon");
                    interactor.ResetElement();
                }
                break;
            default:
                Debug.Log("Failed to interact");
                break;
        }

        
    }
}
