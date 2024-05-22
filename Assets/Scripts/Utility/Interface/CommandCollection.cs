using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
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
    private CharacterBehavior _interactor;
    
    public float CommandTime { get; set; }

    public InteractCommand(IInteract interactObj, CharacterBehavior characterBehavior)
    {
        Init(interactObj, characterBehavior);
    }

    public void Init(IInteract interactObj, CharacterBehavior characterBehavior)
    {
        _interactObj = interactObj;
        _interactor = characterBehavior;
        CommandTime = Time.time;
    }
    
    public void Execute()
    {
        _interactObj.Interact(_interactor);
    }
}