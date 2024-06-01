using UnityEngine;
using UnityEngine.UI;

public class Talkable : TalkInteractable
{
    [TextArea(1, 3)]
    public string[] lines;

    protected override void Update()
    {
        base.Update();

        if (isEntered && Input.GetKeyDown(KeyCode.E) && !DialogueManager.instance.dialogueBox.activeInHierarchy)
        {
            DialogueManager.instance.ShowDialogue(lines);
            interactionPrompt.SetActive(false); // 隐藏提示框当对话开始时
        }
    }
}
