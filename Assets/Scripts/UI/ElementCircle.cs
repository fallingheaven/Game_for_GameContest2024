using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ElementCircle : MonoBehaviour
{
    private List<Element> elements = new List<Element>();
    public Image elementBallImage;
    public float colorTransitionDuration = 1.0f; // 颜色渐变持续时间
    private Coroutine colorTransitionCoroutine;

    public enum Element
    {
        Wind,
        Fire,
        Water,
        Earth,
        Wood
    }

    private Dictionary<Element, Color> elementColors = new Dictionary<Element, Color>()
    {
        { Element.Wind, new Color(0.6f, 0.9f, 0.6f) },
        { Element.Fire, new Color(0.9f, 0.4f, 0.3f) },
        { Element.Water, new Color(0.3f, 0.5f, 0.8f) },
        { Element.Earth, new Color(0.8f, 0.7f, 0.4f) },
        { Element.Wood, new Color(0.1f, 0.6f, 0.2f) }
    };

    private Dictionary<(Element, Element), Color> fusionColors = new Dictionary<(Element, Element), Color>()
    {
        { (Element.Fire, Element.Wood), new Color(0.5f, 0.1f, 0.1f) },
        { (Element.Fire, Element.Water), new Color(0.3f, 0.4f, 0.4f) },
        { (Element.Fire, Element.Earth), new Color(0.4f, 0.2f, 0.1f) },
        { (Element.Water, Element.Earth), new Color(0.3f, 0.2f, 0.1f) }
    };

    private void Start()
    {
        AddElement(Element.Wind);
        Debug.Log("现在有" + elements.Count + "个元素");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && elements.Count < 3)
        {
            AddElement(Element.Fire);
            Debug.Log("吸取了火元素。现在有" + elements.Count + "个元素");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && elements.Count < 3)
        {
            AddElement(Element.Water);
            Debug.Log("吸取了水元素。现在有" + elements.Count + "个元素");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && elements.Count < 3)
        {
            AddElement(Element.Earth);
            Debug.Log("吸取了土元素。现在有" + elements.Count + "个元素");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && elements.Count < 3)
        {
            AddElement(Element.Wood);
            Debug.Log("吸取了木元素。现在有" + elements.Count + "个元素");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ClearFusionElements();
            Debug.Log("清空了元素。现在有" + elements.Count + "个元素");
        }
    }

    private void AddElement(Element newElement)
    {
        if (!elements.Contains(newElement))
        {
            elements.Add(newElement);
            UpdateElementBallColor();
        }
    }

    private void ClearFusionElements()
    {
        elements.Clear();
        AddElement(Element.Wind);
    }

    private void UpdateElementBallColor()
    {
        Color targetColor;

        if (elements.Count == 1)
        {
            targetColor = elementColors[elements[0]];
        }
        else if (elements.Count == 2)
        {
            targetColor = elementColors[elements[1]];
        }
        else if (elements.Count == 3)
        {
            if (fusionColors.TryGetValue((elements[1], elements[2]), out Color fusionColor) ||
                fusionColors.TryGetValue((elements[2], elements[1]), out fusionColor))
            {
                targetColor = fusionColor;
                Debug.Log("触发了元素融合。融合颜色已应用。");
            }
            else
            {
                targetColor = elementColors[elements[2]];
                Debug.Log("没有融合颜色，显示最新吸取元素的颜色。");
            }
        }
        else
        {
            targetColor = elementBallImage.color; // 保持当前颜色
        }

        // 开始颜色渐变协程
        if (colorTransitionCoroutine != null)
        {
            StopCoroutine(colorTransitionCoroutine);
        }
        colorTransitionCoroutine = StartCoroutine(TransitionColor(elementBallImage.color, targetColor
            , colorTransitionDuration));
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
