using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class ShowTargetWhen : MonoBehaviour
{
    public GameObject hiddenObject;
    public InteractableWitheredTree Tree; // if the tree's element is Wind (i.e. the tree has been transformed to high tree), show the target
    void Start()
    {
        hiddenObject = GetComponent<GameObject>();
        
        if (hiddenObject != null)
        {
            hiddenObject.SetActive(false);
        }
    }
    void Update()
    {
        if (hiddenObject != null)
        {
            if (CheckStateCondition())
            {
                hiddenObject.SetActive(true);
            }
            else
            {
                hiddenObject.SetActive(false);
            }
        }
    }

    private bool CheckStateCondition()
    {
        return Tree.getElement() == Element.Wind;
    }
}
