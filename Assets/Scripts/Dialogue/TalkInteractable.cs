using UnityEngine;
using UnityEngine.UI;

public abstract class TalkInteractable : MonoBehaviour
{
    [SerializeField] protected bool isEntered;
    public GameObject interactionPrompt;
    protected Text promptText;

    protected virtual void Start()
    {
        interactionPrompt.SetActive(false); // 确保提示框开始时是隐藏的
        promptText = interactionPrompt.GetComponentInChildren<Text>();  // 获取Text组件
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            isEntered = true;
            ShowPrompt();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger");
            isEntered = false;
            HidePrompt();
        }
    }

    protected virtual void Update()
    {
        // 基类中的Update逻辑
    }

    protected virtual void ShowPrompt()
    {
        promptText.text = gameObject.name;  // 设置提示框文本为物体名称
        interactionPrompt.SetActive(true);
    }

    protected virtual void HidePrompt()
    {
        interactionPrompt.SetActive(false);
    }
}
