using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableBridge : MonoBehaviour, IInteract
{
    private bool _Fixed = false; // _Fixed = false ��ʾ������
    public void Interact(CharacterBehavior interactor)
    {
        if (_Fixed == false) // ���������
        {
            if (interactor.CurrentElement == Element.Soil)
            {
                Debug.Log("�޸�����");
                _Fixed = true;
            }
            else
            {
                Debug.Log("�����𻵣���Ҫ *��* Ԫ�����޸�");
            }
        }
        else // �������޸�
        {
            Debug.Log("��ͨ������");
        }
    }
}
