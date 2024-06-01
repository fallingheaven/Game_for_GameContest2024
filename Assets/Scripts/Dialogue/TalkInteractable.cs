using UnityEngine;
using UnityEngine.UI;

public abstract class TalkInteractable : MonoBehaviour
{
    [SerializeField] protected bool isEntered;
    public GameObject interactionPrompt;
    protected Text promptText;

    protected virtual void Start()
    {
        interactionPrompt.SetActive(false); // ȷ����ʾ��ʼʱ�����ص�
        promptText = interactionPrompt.GetComponentInChildren<Text>();  // ��ȡText���
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
        // �����е�Update�߼�
    }

    protected virtual void ShowPrompt()
    {
        promptText.text = gameObject.name;  // ������ʾ���ı�Ϊ��������
        interactionPrompt.SetActive(true);
    }

    protected virtual void HidePrompt()
    {
        interactionPrompt.SetActive(false);
    }
}
