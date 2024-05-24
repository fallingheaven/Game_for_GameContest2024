using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManger : MonoBehaviour
{
    public static DialogueManger instance;

    public GameObject dialogueBox;
    public Text dialogueText, nameText;
    [TextArea(1, 3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

    }
    private void Start()
    {
        if (dialogueLines != null && dialogueLines.Length > 0 && currentLine >= 0 && currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }

    }


    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0))
            {
                currentLine++;
                if (currentLine < dialogueLines.Length)
                    dialogueText.text = dialogueLines[currentLine];
                else
                {
                    dialogueBox.SetActive(false);
                    FindObjectOfType<PlayerMovement>().canMove = true;
                }
            }
        }
    }
    public void ShowDialogue(string[] _newLines)
    {
        dialogueLines = _newLines;
        currentLine = 0;
        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);
        FindObjectOfType<PlayerMovement>().canMove = false;

    }
}
