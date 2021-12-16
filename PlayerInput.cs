using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] public Transform groundCheck1;
    [SerializeField] public Transform groundCheck2;
    private Rigidbody2D rb2d;
    private bool facingRight = true;
    [SerializeField] private float moveInput = 0;
    [SerializeField] private float moveSpeed = 0.1f;
    private Animator anim;
    public static Vector2 playerPosition;
    public LayerMask ground;
    private float maxMoveSpeed = 2.1f;
    private float accelerationMove = 3.1f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float groundRadius = 0.05f;
    [SerializeField] private float gravity = -1;
    void Start()
    {
        moveInput = 0;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        anim.speed = 1;
    }
    public void MoveGroudAccelerator()
    {
        if (moveInput != 0 && isGrounded)
        {
            if (Math.Abs(moveInput) < maxMoveSpeed) moveInput += accelerationMove * moveInput * Time.deltaTime;
            else moveInput = maxMoveSpeed * Math.Sign(moveInput);
        }
    }
    void FixedUpdate()
    {
        PlayerMove();
    }
    public void PlayerMove()
    {
        MoveGroudAccelerator();
        isGrounded = (Physics2D.OverlapCircle(groundCheck1.position, groundRadius, ground) || Physics2D.OverlapCircle(groundCheck2.position, groundRadius, ground));
        Vector2 tempPos = Vector2.zero;
        tempPos.x += moveInput * moveSpeed;
        tempPos.y += gravity * Time.deltaTime;
        rb2d.MovePosition(rb2d.position + tempPos);
        AnimationMaker();
       // JumpUp();
        playerPosition = transform.position;
    }

    public void AnimationMaker()
    {
        anim.SetFloat("speed", Mathf.Abs(moveInput));
        anim.SetBool("grounded", isGrounded);
        anim.SetFloat("ySpeed", moveInput);
    }
    public void GoLeft()
    {
        if (facingRight)
        {
            Flip();
        }
        moveInput = -1;
    }
    public void GoRight()
    {
        if (!facingRight)
        {
            Flip();
        }
        moveInput = 1;
    }
    public void GoButtonUp()
    {
        moveInput = 0;
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject enemy = coll.gameObject;
        if (enemy.GetComponent<ProtoEnemy>() != null)
        {
            enemy.transform.GetComponent<ProtoEnemy>().EnemyTakingDamage();
        }
    }
}
