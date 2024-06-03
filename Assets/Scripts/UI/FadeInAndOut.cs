using DG.Tweening;
using Event;
using UnityEngine;
using UnityEngine.UI;
using Utility.Interface;

public class FadeInAndOut : MonoBehaviour
{
    public float fadeTime = 0.5f;
    private Image _sprite;
    private Tweener _tweener = null;

    private void Start()
    {
        _sprite = GetComponent<Image>();
        EventManager.Instance.SubscribeEvent<OnFadeIn>(FadeIn);
        EventManager.Instance.SubscribeEvent<OnFadeOut>(FadeOut);
    }

    private void FadeIn(IEventMessage message)
    {
        _tweener = _sprite.DOColor(Color.black, fadeTime);
        
        if (message is not OnFadeIn msg) return;
        
        if (msg.fadeOut)
            _tweener.onComplete += () => _sprite.DOColor(Color.clear, fadeTime);
    }

    private void FadeOut(IEventMessage message)
    {
        _tweener = _sprite.DOColor(Color.clear, fadeTime);
    }
    
    /// <summary>
    /// 仅供淡入测试
    /// </summary>
    public void In()
    {
        var message = new OnFadeIn();
        EventManager.Instance.InvokeEvent(message);
    }

    /// <summary>
    /// 仅供淡出测试
    /// </summary>
    public void Out()
    {
        var message = new OnFadeOut();
        EventManager.Instance.InvokeEvent(message);
    }
}
