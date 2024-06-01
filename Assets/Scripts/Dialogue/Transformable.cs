using UnityEngine;

public class Transformable : TalkInteractable
{
    public string s1;
    public string s2;
    private bool boolean;

    protected override void Start()
    {
        base.Start();
        UpdatePromptText();
    }

    //protected override void Update()
    //{
    //    base.Update();
    //    if (isEntered && Input.GetKeyDown(KeyCode.E))
    //    {
    //        if (isCleaned)
    //        {
    //            // 变成可对话的状态
    //            DialogueManager.instance.ShowDialogue(new string[] { "The cleaned object is now talkable." });
    //            interactionPrompt.SetActive(false);
    //        }
    //        else
    //        {
    //            // 执行清洗操作
    //            isCleaned = true;
    //            UpdatePromptText();
    //        }
    //    }
    //}
    public override void ShowPrompt()
    {
        UpdatePromptText();
        interactionPrompt.SetActive(true);
    }
    public void UpdatePromptText()
    {
        if (!boolean)
        {
            promptText.text = s1;
        }
        else
        {
            promptText.text = s2;
        }
    }
    public void SetState(bool b)
    {
        boolean = b;
        UpdatePromptText();
    }
}
