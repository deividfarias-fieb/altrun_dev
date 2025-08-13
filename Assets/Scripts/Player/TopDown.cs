using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator _playerAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        if (moveInput.sqrMagnitude > 0)
        {
            _playerAnimator.SetIntenger("Movimento", 1);
        }
        else
        {
            _playerAnimator.SetIntenger("Movimento", 0);
        }

        Flip();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void Flip()
    {
        if(moveInput.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if (moveInput.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        } 
    }
}
