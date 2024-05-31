using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class ShowTargetWhen : MonoBehaviour
{
    // public GameObject hiddenObject;
    public InteractableWitheredTree Tree; // if the tree's element is Wind (i.e. the tree has been transformed to high tree), show the target
    private bool flag = false;
    private SpriteRenderer _sr;
    private BoxCollider2D _col;
    void Start()
    {
        // hiddenObject = GetComponent<GameObject>();
        //
        // if (hiddenObject != null)
        // {
        //     Debug.Log(1111);
        //     hiddenObject.SetActive(false);
        // }
        _sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<BoxCollider2D>();
        _sr.enabled = false;
        _col.enabled = false;
    }
    void Update()
    {
        // if (hiddenObject != null)
        // {
        if(flag == false)
        {
            if (CheckStateCondition())
            {
                // hiddenObject.SetActive(true);
                _sr.enabled = true;
                _col.enabled = true;
                flag = true;
            }
        }
            
        // }
    }

    private bool CheckStateCondition()
    {
        // if(Tree.getElement() == Element.Wind)  Debug.Log("Wind");
        return Tree.getElement() == Element.Wind;
    }
}
