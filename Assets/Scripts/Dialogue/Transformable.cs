using UnityEngine;

public class Transformable : TalkInteractable
{
    private bool isCleaned;

    protected override void Start()
    {
        base.Start();
        UpdatePromptText();
    }

    protected override void Update()
    {
        base.Update();
        if (isEntered && Input.GetKeyDown(KeyCode.E))
        {
            if (isCleaned)
            {
                // ��ɿɶԻ���״̬
                DialogueManager.instance.ShowDialogue(new string[] { "The cleaned object is now talkable." });
                interactionPrompt.SetActive(false);
            }
            else
            {
                // ִ����ϴ����
                isCleaned = true;
                UpdatePromptText();
            }
        }
    }

    private void UpdatePromptText()
    {
        if (isCleaned)
        {
            promptText.text = $"{gameObject.name} is now clean and talkable.";
        }
        else
        {
            promptText.text = $"{gameObject.name} is covered in dust.";
        }
    }
}
