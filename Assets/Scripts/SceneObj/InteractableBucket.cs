using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableBucket : MonoBehaviour, IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        Debug.Log("吸收水元素");
        interactor.AbsorbElement(Element.Water);
    }
}
