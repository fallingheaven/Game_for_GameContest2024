using System;
using UnityEngine;
using Utility;
using Utility.Interface;

public class MoveCommand : ICommand
{
    private Vector2 _moveDirection;
    private Action<Vector2> _moveAction;
    
    public float CommandTime { get; set; }

    public MoveCommand(Vector2 moveDirection, Action<Vector2> moveAction)
    {
        Init(moveDirection, moveAction);
    }

    public void Init(Vector2 moveDirection, Action<Vector2> moveAction)
    {
        _moveDirection = moveDirection;
        _moveAction = moveAction;
        CommandTime = Time.time;
    }
    
    public void Execute()
    {
        // Debug.Log($"moveCommand Invoke");
        _moveAction.Invoke(_moveDirection);
    }
}

public class InteractCommand : ICommand
{
    private IInteract _interactObj;
    private Element _element;
    
    public float CommandTime { get; set; }

    public InteractCommand(IInteract interactObj, Element element = Element.Null)
    {
        Init(interactObj, element);
    }

    public void Init(IInteract interactObj, Element element = Element.Null)
    {
        _interactObj = interactObj;
        _element = element;
        CommandTime = Time.time;
    }
    
    public void Execute()
    {
        _interactObj.Interact(_element);
    }
}