using System;
using System.Collections.Generic;
using System.Linq;
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
    public Vector3 offset;
    
    private bool _moving;
    private Vector2 _faceDir = Vector2.down;
    public Vector2 FaceDir => _faceDir;

    private readonly Dictionary<ECommand, ICommand> _commandDictionary = new();

    private ElementUI _elementUI;

    public Sprite moveUp;
    public Sprite moveDown;
    public Sprite moveLeft;
    public Sprite moveRight;
    private SpriteRenderer _sr;

    public AudioPlayEvent moveClip;

    public Dictionary<Element, int> elementAudioDictionary = new Dictionary<Element, int>()
    {
        { Element.Wind , 0}, { Element.Fire, 1}, { Element.Water, 2 }, { Element.Soil, 3 }, { Element.Wood, 4 },
        { Element.Rock, 5 }, { Element.Ember, 6 }, { Element.Steam, 7 }, { Element.Wetland, 8 }
    };
    public AudioPlayEvent[] fxAudio;
    
    private void Start()
    {
        _commandDictionary[ECommand.Move] = new MoveCommand(Vector2.zero, null);
        _commandDictionary[ECommand.Interact] = new InteractCommand(null, this);
        _elementUI = GetComponent<ElementUI>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var position = transform.position;
        var colCenter = new Vector2(position.x + _faceDir.x + offset.x, position.y + _faceDir.y + offset.y);
        CheckInteractObj(colCenter);
        
        var moveDir = InputSystem.PlayerMoveInput;
        if (moveDir != Vector2.zero)
        {
            _faceDir = moveDir.normalized;
        }
        
        if (moveDir != Vector2.zero && !_moving)
        {
            var checkCenter = new Vector2(position.x + moveDir.x + offset.x, position.y + moveDir.y + offset.y);
            if (CheckBoundary(checkCenter))
            {
                var moveCommand = _commandDictionary[ECommand.Move] as MoveCommand;
                moveCommand?.Init(moveDir, Move);
                            
                InputManager.Instance.AddCommand(new MoveCommand(moveDir, Move));
            }
        }
        
        if (InputSystem.Interact)
            EventManager.Instance.InvokeEvent(fxAudio[elementAudioDictionary[CurrentElement]]);
        
        if (InputSystem.Interact && _interactObj != null)
        {
            var interactCommand = _commandDictionary[ECommand.Interact] as InteractCommand;
            interactCommand?.Init(_interactObj, this);

            InputManager.Instance.AddCommand(interactCommand);
        }

        if (InputSystem.Release)
        {
            ResetElement();
        }

        if (moveDir.y >= 1f)
        {
            _sr.sprite = moveUp;
        }
        else if (moveDir.y <= -1f)
        {
            _sr.sprite = moveDown;
        }
        else
        {
            if (moveDir.x >= 1f)
            {
                _sr.sprite = moveRight;
            }
            else if (moveDir.x <= -1f)
            {
                _sr.sprite = moveLeft;
            }
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

    public bool CheckBoundary(Vector2 colCenter)
    {
        var colliders = Physics2D.OverlapBoxAll(colCenter, Vector2.one * 0.8f, 0);
        return colliders.Length == 0 || colliders.All(col => !col.transform.CompareTag("Darkness") && !col.transform.CompareTag("Boundary") && (interactableMask & (1 << col.gameObject.layer)) == 0);
    }

    private void Move(Vector2 moveDir)
    {
        if (_moving) return;
        // Debug.Log("move");
        
        _moving = true;
        var targetPos = transform.position + new Vector3(moveDir.x, moveDir.y) * moveDis;
        transform.DOMove(targetPos, 0.3f, false).SetEase(Ease.InOutExpo).onComplete += () => { _moving = false;};

        EventManager.Instance.InvokeEvent(moveClip);
    }

    public void ResetElement()
    {
        CurrentElement = Element.Wind;
        _elementUI.UpdateElementBallColor();
    }

    public bool AbsorbElement(Element targetElement)
    {
        if (CurrentElement is Element.Wind or >= Element.Rock)//没有元素或是融合元素
        {
           CurrentElement = targetElement;
            _elementUI.UpdateElementBallColor();
           Debug.Log(CurrentElement);
           return true;
        }
        else//融合
        {
            Element fusionResult;
            if (Fusion.FusionMap[CurrentElement].TryGetValue(targetElement, out fusionResult))
            {
                CurrentElement = fusionResult;
                _elementUI.UpdateElementBallColor();
                Debug.Log(CurrentElement);
                return true;
            }
            else//没有相应的融合
            {
                Debug.Log("No fusion.");
                return false;
            }
        }

        //Debug.Log(CurrentElement);
    }

    public void Die()
    {
        LevelManager.Instance.RefreshLevel();
    }

    private void OnDrawGizmos()
    {
        var colCenter = new Vector3(transform.position.x + _faceDir.x + offset.x, transform.position.y + _faceDir.y + offset.y, 0f);
        Gizmos.DrawCube(colCenter, Vector3.one * 0.8f);
    }
}
