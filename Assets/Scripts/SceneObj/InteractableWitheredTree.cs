using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableWitheredTree : MonoBehaviour, IInteract
{
    // use Element to represent the withered tree's current state
    private Element nowElement = Element.Wood; // default element
    public Element getElement() { return nowElement; }

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
                else
                {
                    Debug.Log("The tree is withered, you need *WATER* and *SOIL* to make some changes");
                }
                break;
            case Element.Wetland:
                if(interactor.CurrentElement == Element.Ember)
                {
                    nowElement = interactor.CurrentElement;
                    Debug.Log("Transform to Ember");
                    interactor.ResetElement();
                }
                else
                {
                    Debug.Log("Now you need *FIRE* and *WOOD* to accelerate the growth of the tree");
                }
                break;
            case Element.Ember:
                if(interactor.CurrentElement == Element.Wind)
                {
                    nowElement = interactor.CurrentElement;
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
