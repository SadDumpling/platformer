using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    public delegate void OnAnimationChange(PlayerAnimation anCh);
    static public event OnAnimationChange _onAnimationChange;
    private Rigidbody2D rb2d;
    private PlayerGroundcheck groundcheck;
    private PlayerMove playerMove;
    private PlayerCollisions collisions;

    void Start()
    {
        _onAnimationChange += AnimationMaker;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        groundcheck = GetComponent<PlayerGroundcheck>();
        playerMove = GetComponent<PlayerMove>();
        collisions = GetComponent<PlayerCollisions>();
    }
    public void AnimationMaker(PlayerAnimation anCh)
    {
        if (!collisions.PlayerDie)
        {
            anim.SetFloat("speed", Mathf.Abs(PlayerMove.MoveInput));
            anim.SetBool("grounded", groundcheck.GroundCheck());
            anim.SetFloat("ySpeed", rb2d.velocity.y);
        }
        else
        {
            anim.SetBool("die", collisions.PlayerDie);
        }
    }
    public void Update()
    {
        _onAnimationChange?.Invoke(this);
    }
}
