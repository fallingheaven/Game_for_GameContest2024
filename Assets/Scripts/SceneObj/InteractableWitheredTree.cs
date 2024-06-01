using Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableWitheredTree : MonoBehaviour, IInteract
{
    public Sprite witheredTree;
    public Sprite littleTree;
    public Sprite bigTree;

    private SpriteRenderer _sr;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = witheredTree;
    }


    // use Element to represent the withered tree's current state
    public Element nowElement = Element.Wood; // default element
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
                    _sr.sprite = littleTree;
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
                    StartCoroutine(GetBig(interactor));
                }
                break;
            default:
                Debug.Log("Failed to interact");
                break;
        }

        
    }

    private IEnumerator GetBig(CharacterBehavior interactor)
    {
        var message1 = new OnFadeIn();
        EventManager.Instance.InvokeEvent(message1);

        yield return new WaitForSeconds(0.55f);
        nowElement = interactor.CurrentElement;
        Debug.Log("Transform to high tree");
        Debug.Log("Change the Icon");
        _sr.sprite = bigTree;
        interactor.ResetElement();

        var message2 = new OnFadeOut();
        EventManager.Instance.InvokeEvent(message2);
    }
}
