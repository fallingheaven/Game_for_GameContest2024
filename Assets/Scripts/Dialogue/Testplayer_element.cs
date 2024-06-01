using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class ElementUI : MonoBehaviour
{
    //private List<Element> elements = new List<Element>();
    public Image elementBallImage;
    public Text elementInfoText;
    public float colorTransitionDuration = 1.0f; // ��ɫ�������ʱ��
    private Coroutine colorTransitionCoroutine;

    private CharacterBehavior _character;

    private Dictionary<Element, string> elementDescriptions = new Dictionary<Element, string>()
    {
        { Element.Wind, "��" },
        { Element.Fire, "��" },
        { Element.Water, "ˮ" },
        { Element.Soil, "��" },
        { Element.Wood, "ľ" }
    };
    private Dictionary<Element, Color> elementColors = new Dictionary<Element, Color>()
    {
        { Element.Wind, new Color(0.6f, 0.7f, 0.6f) },
        { Element.Fire, new Color(0.8f, 0.36f, 0.36f) },
        { Element.Water, new Color(0.3f, 0.5f, 0.8f) },
        { Element.Soil, new Color(0.8f, 0.7f, 0.4f) },
        { Element.Wood, new Color(0.1f, 0.6f, 0.2f) }
    };

    private Dictionary<Element, Color> fusionColors = new Dictionary<Element, Color>()
    {
        { (Element.Ember), new Color(0.5f, 0.1f, 0.1f) },
        { (Element.Steam), new Color(0.3f, 0.4f, 0.4f) },
        { (Element.Rock), new Color(0.4f, 0.2f, 0.1f) },
        { (Element.Wetland), new Color(0.3f, 0.2f, 0.1f) }
    };

    private Dictionary<Element, string> fusionDescriptions = new Dictionary<Element, string>()
    {
        { (Element.Ember), "���" }, // ���ľ�ںϺ������
        { (Element.Steam), "����" }, // ���ˮ�ںϺ������
        { (Element.Rock), "�մ�" }, // ������ںϺ������
        { (Element.Wetland), "����" } // ˮ�����ںϺ������
    };

    private void Start()
    {
        //AddElement(Element.Wind);
        //Debug.Log("������" + elements.Count + "��Ԫ��");
        _character = GetComponent<CharacterBehavior>();
        UpdateElementBallColor();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1)&&elements.Count<3)
        //{
        //    AddElement(Element.Fire);
        //    Debug.Log("��ȡ�˻�Ԫ�ء�������" + elements.Count + "��Ԫ��");
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2) && elements.Count < 3)
        //{
        //    AddElement(Element.Water);
        //    Debug.Log("��ȡ��ˮԪ�ء�������" + elements.Count + "��Ԫ��");
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3) && elements.Count < 3)
        //{
        //    AddElement(Element.Earth);
        //    Debug.Log("��ȡ����Ԫ�ء�������" + elements.Count + "��Ԫ��");
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha4) && elements.Count < 3)
        //{
        //    AddElement(Element.Wood);
        //    Debug.Log("��ȡ��ľԪ�ء�������" + elements.Count + "��Ԫ��");
        //}

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    ClearFusionElements();
        //    Debug.Log("�����Ԫ�ء�������" + elements.Count + "��Ԫ��");
        //}
    }

    //private void AddElement(Element newElement)
    //{
    //    if (!elements.Contains(newElement))
    //    {
    //        elements.Add(newElement);
    //        UpdateElementBallColor();
    //        UpdateElementInfoText();
    //    }
    //}

    //private void ClearFusionElements()
    //{
    //    elements.Clear();
    //    AddElement(Element.Wind);
    //}

    public void UpdateElementBallColor()
    {
        Color targetColor;
        var element = _character.CurrentElement;
        //if (elements.Count == 1)
        //{
        //    targetColor = elementColors[elements[0]];
        //}
        //else if (elements.Count == 2)
        //{
        //    targetColor = elementColors[elements[1]];
        //}
        //else if (elements.Count == 3)
        //{
            if (fusionColors.TryGetValue(element, out Color fusionColor) ||
                fusionColors.TryGetValue(element, out fusionColor))
            {
                targetColor = fusionColor;
                Debug.Log("������Ԫ���ںϡ��ں���ɫ��Ӧ�á�");
            }
            else
            {
                targetColor = elementColors[element];
                Debug.Log("û���ں���ɫ����ʾ������ȡԪ�ص���ɫ��");
            }
        //}
        //else
        //{
        //    targetColor = elementBallImage.color; // ���ֵ�ǰ��ɫ
        //}

        //targetColor.a = 0.50f; // ����͸����Ϊ100/255
        // ��ʼ��ɫ����Э��
        if (colorTransitionCoroutine != null)
        {
            StopCoroutine(colorTransitionCoroutine);
        }
        colorTransitionCoroutine = StartCoroutine(TransitionColor(elementBallImage.color, targetColor
            , colorTransitionDuration));

        UpdateElementInfoText();
    }
    private void UpdateElementInfoText()
    {
        var element = _character.CurrentElement;
        //if (elements.Count == 1)
        //{
        //    elementInfoText.text = elementDescriptions[elements[0]];
        //}
        //else if(elements.Count == 2)
        //{
        //    elementInfoText.text = elementDescriptions[elements[1]];
        //}
        //else if(elements.Count == 3)
        //{
            if (fusionDescriptions.TryGetValue(element, out string description) ||
                fusionDescriptions.TryGetValue(element, out description))
            {
                elementInfoText.text = description;
            }
            else
            {
                elementInfoText.text = elementDescriptions[element];
            }
        //}
    }
    private IEnumerator TransitionColor(Color fromColor, Color toColor, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elementBallImage.color = Color.Lerp(fromColor, toColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elementBallImage.color = toColor;
    }
}
