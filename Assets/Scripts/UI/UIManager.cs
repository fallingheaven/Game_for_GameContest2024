using Event;
using Utility.CustomClass;

public class UIManager : Singleton<UIManager>
{
    public void FadeIn()
    {
        var message = new OnFadeIn();
        EventManager.Instance.InvokeEvent(message);
    }

    public void FadeOut()
    {
        var message = new OnFadeOut();
        EventManager.Instance.InvokeEvent(message);
    }
}
