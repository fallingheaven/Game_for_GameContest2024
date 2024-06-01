using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Utility;
using Utility.Interface;

public class InteractableTorch : MonoBehaviour,IInteract
{
    public float lightRadius;
    public Vector3 offset;
        
    public void Interact(CharacterBehavior interactor)
    {
        if (interactor.CurrentElement == Element.Fire)
        {
            Debug.Log("点燃火把，照亮下一解密要素。");

            var colliders = Physics2D.CircleCastAll(transform.position + offset, lightRadius, Vector2.zero);
            foreach (var col in colliders.Where(col => col.transform.CompareTag("Darkness")))
            {
                var sprite = col.transform.GetComponent<SpriteRenderer>();
                sprite.DOColor(Color.clear, 0.5f).onComplete += () => {Destroy(sprite.gameObject);};
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + offset, lightRadius);
    }
}
