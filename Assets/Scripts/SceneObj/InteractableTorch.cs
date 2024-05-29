using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableTorch : MonoBehaviour,IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        if (interactor.CurrentElement == Element.Fire)
        {
            Debug.Log("点燃火把，照亮下一解密要素。");
        }
    }
}
