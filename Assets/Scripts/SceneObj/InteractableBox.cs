using DG.Tweening;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableBox : MonoBehaviour,IInteract
{
    private bool _moving = false;
    public void Interact(CharacterBehavior interactor)
    {
        if (_moving) return;
        
        if (interactor.CurrentElement == Element.Wind)
        {
            Debug.Log("推动木箱。");
            _moving = true;
            var moveDir = interactor.FaceDir;
            
            var targetPos = transform.position + new Vector3(moveDir.x, moveDir.y) * interactor.moveDis;
            transform.DOMove(targetPos, 0.3f, false).SetEase(Ease.InOutExpo).onComplete += () => { _moving = false;};
        }
    }
}
