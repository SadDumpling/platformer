using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool jumpButtonDownBool = false;
    private PlayerGroundcheck groundcheck;
    [SerializeField] private int jumpForceCount = 9;
    [SerializeField] private float jumpForceMultiplier = 8.5f;
    [SerializeField] private int tempJumpCount = 0;
    [SerializeField] private bool doJump = false;
    [SerializeField] private float jumpPower = 30000;
    public bool JumpButtonDownBool
    {
        get
        {
            return jumpButtonDownBool;
        }
    }
    public float JumpPower
    {
        get
        {
            return jumpPower;
        }
    }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        groundcheck = GetComponent<PlayerGroundcheck>();
    }
    public void JumpButtonDown()
    {
        jumpButtonDownBool = true;
        bool isGrounded = groundcheck.GroundCheck();
        if (isGrounded)
        {
            doJump = true;
            rb2d.AddForce(new Vector2(0, jumpPower));
        }
        PlayerManager._onPlayerAction += JumpUp;
    }
    public void JumpButtonUp()
    {
        doJump = false;
        jumpButtonDownBool = false;
        tempJumpCount = 0;
        PlayerManager._onPlayerAction -= JumpUp;
    }
    public void JumpUp(PlayerManager plMn)
    {
        if (doJump && tempJumpCount < jumpForceCount)
        {
            rb2d.AddForce(new Vector2(0, jumpPower * jumpForceMultiplier * Time.deltaTime));
            tempJumpCount++;            
        }
    }
}
