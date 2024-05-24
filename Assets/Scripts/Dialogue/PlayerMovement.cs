using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
  private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private float moveSpeed;
    private float moveH, moveV;
    public bool canMove = true;
    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        sr= GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        moveH = Input.GetAxis("Horizontal") * moveSpeed;
        moveV=Input.GetAxis("Vertical")*moveSpeed;
        if(canMove )
        Flip();
    }
    private void FixedUpdate()
    {
        if (canMove)
            rb.velocity = new Vector2(moveH, moveV);
        else
            rb.velocity =Vector2.zero;
    }
    private void Flip()
    {
        if (moveH > 0)
        {
            transform.eulerAngles=new Vector3(0,0,0);
        }else if (moveH < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
