using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableTarget : MonoBehaviour, IInteract
{
    public void Interact(Element element = Element.Null)
    {
        Debug.Log("到下一关");
        LevelManager.Instance.ToNextLevel();
    }
}
