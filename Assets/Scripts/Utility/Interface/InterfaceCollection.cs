using Utility;

namespace Utility.Interface
{
    /// <summary>
    /// 事件消息接口类
    /// </summary>
    public interface IEventMessage
    {
        
    }

    /// <summary>
    /// 命令接口类
    /// </summary>
    public interface ICommand
    {
        public float CommandTime { get; set; }
        public void Execute();
    }

    /// <summary>
    /// 交互接口类
    /// </summary>
    public interface IInteract
    {
        public void Interact(CharacterBehavior interactor);
    }

    public abstract class IObjectPool
    {
        public string tag;
        public int count;
        
        public abstract void Enqueue(object newObj);
        public abstract object Dequeue();
    }
}
