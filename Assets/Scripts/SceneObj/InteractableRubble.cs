using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableRubble : MonoBehaviour, IInteract
{
    public void Interact(CharacterBehavior interactor)
    {
        Debug.Log("������Ԫ��");
        interactor.AbsorbElement(Element.Soil);
        Debug.Log("��ʯ����ʧ");
    }
}
