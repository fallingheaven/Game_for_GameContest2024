using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableChest : MonoBehaviour, IInteract
{
    public void Interact(Element element = Element.Null)
    {
        Debug.Log($"open the chest!");
    }
}
