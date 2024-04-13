using UnityEngine;
using Utility.Interface;

public class MoveCommand : ICommand
{
    private Vector2 _moveDirection;
    private Transform _moveObj;
    
    public float CommandTime { get; set; }

    public MoveCommand(Vector2 moveDirection, Transform moveObj)
    {
        _moveDirection = moveDirection;
        _moveObj = moveObj;
    }
    
    public void Execute()
    {
        // TODO: 对象移动方法调用
    }
}

public class InteractCommand : ICommand
{
    private IInteract _interactObj;
    public float CommandTime { get; set; }

    public InteractCommand(IInteract interactObj)
    {
        _interactObj = interactObj;
    }

    public void Execute()
    {
        _interactObj.Interact();
    }
}