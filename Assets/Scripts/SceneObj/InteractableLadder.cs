using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableLadder : MonoBehaviour, IInteract
{
    public Sprite brokenLadder;
    public Sprite brandLadder;
    private SpriteRenderer _sr;
    private bool _Fixed = false; // _Fixed = false

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        if (_Fixed) _sr.sprite = brandLadder;
        else
        {
            _sr.sprite = brokenLadder;
        }
    }

    public void Interact(CharacterBehavior interactor)
    {
        if (_Fixed == false)
        {
            if (interactor.CurrentElement == Element.Wood)
            {
                Debug.Log("fixed the ladder successfully");
                _Fixed = true;
                _sr.sprite = brandLadder;
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
