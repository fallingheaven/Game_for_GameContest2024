using Utility.Interface;

namespace Event
{
    public class OnFadeIn : IEventMessage
    {
        public bool fadeOut;
    }

    public class OnFadeOut : IEventMessage
    {
        
    }
}