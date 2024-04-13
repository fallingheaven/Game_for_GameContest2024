namespace Utility.Interface
{
    public interface IEventMessage
    {
        
    }

    public interface ICommand
    {
        public float CommandTime { get; set; }
        public void Execute();
    }

    public interface IInteract
    {
        public void Interact();
    }
}
