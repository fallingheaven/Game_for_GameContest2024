using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableBox : MonoBehaviour,IInteract
{
    public Sprite box1;
    public Sprite box2;
    private bool _moving = false;
    private SpriteRenderer _sr;
    private BoxCollider2D _col;
    public Transform hole;
    private bool _isDropped = false;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<BoxCollider2D>();
        _sr.sprite = box1;
    }

    private void Update()
    {
        if (!_isDropped)
        {
            if (transform.position == hole.position)
            {
                _sr.sprite = box2;
                _col.enabled = false;
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }


    public void Interact(CharacterBehavior interactor)
    {
        if (_moving) return;
        var offset = interactor.offset;
        var moveDir = interactor.FaceDir;
        var position = transform.position;
        var checkCenter = new Vector2(position.x + moveDir.x + offset.x, position.y + moveDir.y + offset.y);
        if (!interactor.CheckBoundary(checkCenter)) return;
        
        if (interactor.CurrentElement == Element.Wind)
        {
            Debug.Log("推动木箱。");
            _moving = true;
            
            Vector2 targetPos;
            
            if (moveDir.x != 0 && moveDir.y != 0)
                targetPos = transform.position + new Vector3(moveDir.x, moveDir.y) * (interactor.moveDis * Mathf.Sqrt(2));
            else
                targetPos = transform.position + new Vector3(moveDir.x, moveDir.y) * interactor.moveDis;
            
            transform.DOMove(targetPos, 0.3f, false).SetEase(Ease.InOutExpo).onComplete += () => { _moving = false;};
        }
    }

   
    
    
    
}
