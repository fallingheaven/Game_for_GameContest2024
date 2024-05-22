using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utility;
using Utility.Interface;

public class CharacterBehavior : MonoBehaviour
{
    [Tooltip("一次移动的距离")] public float moveDis;

    private IInteract _interactObj;
    public Element CurrentElement { get; private set; }
    public LayerMask interactableMask;
    
    private bool _moving;
    private Vector2 _faceDir = Vector2.down;

    private readonly Dictionary<ECommand, ICommand> _commandDictionary = new();

    private void Start()
    {
        _commandDictionary[ECommand.Move] = new MoveCommand(Vector2.zero, null);
        _commandDictionary[ECommand.Interact] = new InteractCommand(null, this);
    }

    private void Update()
    {
        var position = transform.position;
        var colCenter = new Vector2(position.x + _faceDir.x - 0.5f, position.y + _faceDir.y + 0.5f);
        CheckInteractObj(colCenter);
        
        var moveDir = InputSystem.PlayerMoveInput;
        if (moveDir != Vector2.zero && !_moving)
        {
            var checkCenter = new Vector2(position.x + moveDir.x - 0.5f, position.y + moveDir.y + 0.5f);
            if (!CheckBoundary(checkCenter))
            {
                var moveCommand = _commandDictionary[ECommand.Move] as MoveCommand;
                moveCommand?.Init(moveDir, Move);
                            
                InputManager.Instance.AddCommand(new MoveCommand(moveDir, Move));
            }
        }
        
        if (InputSystem.Interact && _interactObj != null)
        {
            var interactCommand = _commandDictionary[ECommand.Interact] as InteractCommand;
            interactCommand?.Init(_interactObj, this);

            InputManager.Instance.AddCommand(interactCommand);
        }
        
    }

    private void CheckInteractObj(Vector2 colCenter)
    {
        var col = Physics2D.OverlapBox(colCenter, Vector2.one * 0.8f, 0, interactableMask);
        if (col)
        {
            _interactObj = col.GetComponent<IInteract>();
        }
        else
        {
            _interactObj = null;
        }
    }

    private bool CheckBoundary(Vector2 colCenter)
    {
        var col = Physics2D.OverlapBox(colCenter, Vector2.one * 0.8f, 0, interactableMask);
        return col;
    }

    private void Move(Vector2 moveDir)
    {
        if (_moving) return;
        // Debug.Log("move");
        
        _moving = true;
        var targetPos = transform.position + new Vector3(moveDir.x, moveDir.y) * moveDis;
        transform.DOMove(targetPos, 0.3f, false).SetEase(Ease.InOutExpo).onComplete += () => { _moving = false;};

        _faceDir = moveDir.normalized;
    }

    public void AbsorbElement(Element targetElement)
    {
        if (CurrentElement != Element.Wind) return;
        
        CurrentElement = targetElement;
        // TODO: 融合元素
    }

    public void Die()
    {
        LevelManager.Instance.RefreshLevel();
    }

    private void OnDrawGizmos()
    {
        var colCenter = new Vector3(transform.position.x + _faceDir.x - 0.5f, transform.position.y + _faceDir.y + 0.5f, 0f);
        Gizmos.DrawCube(colCenter, Vector3.one * 0.8f);
    }
}
