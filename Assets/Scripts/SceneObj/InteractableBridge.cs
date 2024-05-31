using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableBridge : MonoBehaviour, IInteract
{
    public Sprite brokenBridge;
    public Sprite brandBridge;
    private SpriteRenderer _sr;
    private BoxCollider2D _col;
    private bool _Fixed = false; // _Fixed = false

    private void Start()
    {
        _col = GetComponent<BoxCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        if (_Fixed) _sr.sprite = brandBridge;
        else
        {
            _sr.sprite = brokenBridge;
            _col.enabled = false;
        }
    }

    public void Interact(CharacterBehavior interactor)
    {
        if (_Fixed == false)
        {
            if (interactor.CurrentElement == Element.Soil)
            {
                Debug.Log("fixed the bridge successfully");
                _Fixed = true;
                _sr.sprite = brandBridge;
                _col.enabled = false;
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
