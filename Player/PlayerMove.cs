using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private PlayerGroundcheck groundcheck;
    private PlayerCollisions collisions;
    private float moveSpeed = 12;
    private static float moveInput = 0;
    private bool facingRight = true;
    public static Vector2 playerPosition;
    private float maxMoveSpeed = 2.1f;
    private float accelerationMove = 3.1f;
    private float skidPower = 0.6f;

    public static float MoveInput
    {
        get
        {
            return moveInput;
        }
    }
    void Start()
    {
        moveInput = 0;
        rb2d = GetComponent<Rigidbody2D>();
        groundcheck = GetComponent<PlayerGroundcheck>();
        collisions = GetComponent<PlayerCollisions>();
    }
    public void MoveGroudAccelerator()
    {
        bool isGrounded = groundcheck.GroundCheck();
        if (isGrounded)
        {
            if (Math.Abs(moveInput) < maxMoveSpeed) moveInput += accelerationMove * moveInput * Time.deltaTime;
            else moveInput = maxMoveSpeed * Math.Sign(moveInput);
        }
    }
    public void MakePlayerMove(PlayerManager plMn)
    {
        bool playerDie = collisions.PlayerDie;
        if (moveInput != 0 && !playerDie)
        {
            MoveGroudAccelerator();
            rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
        }
    }
    public void GoLeft()
    {
        if (facingRight)
        {
            Flip();
        }
        moveInput = -1;
        PlayerManager._onPlayerAction += MakePlayerMove;
    }
    public void GoRight()
    {
        if (!facingRight)
        {
            Flip();
        }
        moveInput = 1;
        PlayerManager._onPlayerAction += MakePlayerMove;
    }
    public void GoButtonUp()
    {
        rb2d.velocity = new Vector2(moveInput * skidPower * moveSpeed, rb2d.velocity.y);
        moveInput = 0;
        PlayerManager._onPlayerAction -= MakePlayerMove;
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void FixedUpdate()
    {
        if(!collisions.PlayerDie) playerPosition = transform.position;
    }
}
