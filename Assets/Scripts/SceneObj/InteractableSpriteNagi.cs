using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableSpriteNagi : MonoBehaviour, IInteract
{
    public Sprite sp;
    public void Interact(CharacterBehavior interactor)
    {
        interactor.ResetElement();
        Debug.Log("You've got the permanent Wind element!");
        // var obj = interactor.gameObject;
        // Debug.Log(obj.name);
        var sr = interactor.transform.GetChild(0).GetComponent<SpriteRenderer>();
        // Debug.Log(sr);
        sr.enabled = true;
        sr.sprite = sp;
        Destroy(gameObject);
    }
}
