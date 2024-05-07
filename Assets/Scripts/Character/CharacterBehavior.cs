using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utility;
using Utility.Interface;

public class CharacterBehavior : MonoBehaviour
{
    [Tooltip("一次移动的距离")] public float moveDis;

    private IInteract _interactObj;
    public Element currentElement;
    
    private bool _moving;

    private readonly Dictionary<ECommand, ICommand> _commandDictionary = new();

    private void Start()
    {
        _commandDictionary[ECommand.Move] = new MoveCommand(Vector2.zero, null);
        _commandDictionary[ECommand.Interact] = new InteractCommand(null);
    }

    private void Update()
    {
        var moveDir = InputSystem.PlayerMoveInput;
        if (moveDir != Vector2.zero && !_moving)
        {
            var moveCommand = _commandDictionary[ECommand.Move] as MoveCommand;
            moveCommand?.Init(moveDir, Move);
            
            InputManager.Instance.AddCommand(new MoveCommand(moveDir, Move));
        }
        
        if (InputSystem.Interact && _interactObj != null)
        {
            var interactCommand = _commandDictionary[ECommand.Interact] as InteractCommand;
            interactCommand?.Init(_interactObj);

            InputManager.Instance.AddCommand(interactCommand);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("IInteract"))
        {
            _interactObj = col.GetComponent<IInteract>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("IInteract"))
        {
            _interactObj = null;
        }
    }

    private void Move(Vector2 moveDir)
    {
        if (_moving) return;
        // Debug.Log("move");
        
        _moving = true;
        var targetPos = transform.position + new Vector3(moveDir.x, moveDir.y) * moveDis;
        transform.DOMove(targetPos, 0.3f, false).SetEase(Ease.InOutExpo).onComplete += () => { _moving = false;};
    }

    public void SwitchElement(Element targetElement)
    {
        currentElement = targetElement;
    }

    public void Die()
    {
        LevelManager.Instance.RefreshLevel();
    }
}
