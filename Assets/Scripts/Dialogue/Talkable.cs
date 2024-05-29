using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Talkable : MonoBehaviour
{ 
    [SerializeField] private bool isEntered;
    [TextArea(1, 3)]
    public string[] lines;

    public GameObject interactionPrompt;
    private void Start()
    {
        interactionPrompt.SetActive(false); // ȷ����ʾ��ʼʱ�����ص�
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isEntered=true;
            interactionPrompt.SetActive(true); // ��ʾ��ʾ��
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = false;
            interactionPrompt.SetActive(false); // ������ʾ��
        }
    
    }

    private void Update()
    {
        if (isEntered && Input.GetKeyDown(KeyCode.E) && !DialogueManger.instance.dialogueBox.activeInHierarchy)
        {
            DialogueManger.instance.ShowDialogue(lines);
            interactionPrompt.SetActive(false); // ������ʾ�򵱶Ի���ʼʱ
        }
       
    }
}
