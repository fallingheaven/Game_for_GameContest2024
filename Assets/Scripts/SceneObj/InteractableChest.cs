using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableChest : MonoBehaviour, IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        Debug.Log($"open the chest!");
    }
}
