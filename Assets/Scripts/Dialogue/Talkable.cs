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
        interactionPrompt.SetActive(false); // 确保提示框开始时是隐藏的
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isEntered=true;
            interactionPrompt.SetActive(true); // 显示提示框
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = false;
            interactionPrompt.SetActive(false); // 隐藏提示框
        }
    
    }

    private void Update()
    {
        if (isEntered && Input.GetKeyDown(KeyCode.E) && !DialogueManger.instance.dialogueBox.activeInHierarchy)
        {
            DialogueManger.instance.ShowDialogue(lines);
            interactionPrompt.SetActive(false); // 隐藏提示框当对话开始时
        }
       
    }
}
