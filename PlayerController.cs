using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Collider2D col2d;
    private float moveSpeed = 12;
    [SerializeField] private float moveInput = 0;
    private bool facingRight = true;
    private Animator anim;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] public Transform groundCheck1;
    [SerializeField] public Transform groundCheck2;
    [SerializeField] private float groundRadius = 0.05f;
    [SerializeField] private float jumpPower = 30000;
    [SerializeField] private int jumpForceCount = 7;
    [SerializeField] private int tempJumpCount = 0;
    [SerializeField] private bool doJump = false;
    public static Vector2 playerPosition;
    private float maxMoveSpeed = 2.1f;
    private float accelerationMove = 3.1f;
    public LayerMask ground;
    private bool jumpButtonDown = false;
    private bool playerDie = false;
    void Start()
    {
        moveInput = 0;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
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
    public void AnimationMaker()
    {
        anim.SetFloat("speed", Mathf.Abs(moveInput));
        anim.SetBool("grounded", isGrounded);
        anim.SetFloat("ySpeed", rb2d.velocity.y);
    }
    public void JumpUp()
    {
        if (doJump && tempJumpCount < jumpForceCount)
        {
            rb2d.AddForce(new Vector2(0, jumpPower * 11 * Time.deltaTime));
            tempJumpCount++;
        }
    }
    public void PlayerMove()
    {
        if(!playerDie)
        {
            MoveGroudAccelerator();
            isGrounded = (Physics2D.OverlapCircle(groundCheck1.position, groundRadius, ground) || Physics2D.OverlapCircle(groundCheck2.position, groundRadius, ground));
            rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
            AnimationMaker();
            JumpUp();
            playerPosition = transform.position;
        }
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
    public void JumpButtonDown()
    {
        jumpButtonDown = true;
        if (isGrounded)
        {
            doJump = true;
            anim.SetBool("grounded", false);
            rb2d.AddForce(new Vector2(0, jumpPower));
        }
    }
    public void JumpButtonUp()
    {
        doJump = false;
        jumpButtonDown = false;
        tempJumpCount = 0;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject enemy = coll.gameObject;
        if (enemy.GetComponent<ProtoEnemy>() != null)
        {
            if (groundCheck1.position.y > enemy.transform.position.y)
            {
                Debug.Log("EnemyMustDie");
                float tempJumpForce = 1;
                tempJumpForce = jumpButtonDown == true ? 2.2f : 1.3f;
                rb2d.AddForce(new Vector2(0, jumpPower * tempJumpForce));
                enemy.transform.GetComponent<ProtoEnemy>().EnemyTakingDamage();
            }
            else
            {
                gameObject.layer = 11;
                rb2d.AddForce(new Vector2(0, jumpPower*1.5f ));
                Debug.Log("PlayerMustDie");
                playerDie = true;
                anim.SetBool("die", playerDie);
            }
        }
    }
    void FixedUpdate()
    {
        PlayerMove();
    }
}
