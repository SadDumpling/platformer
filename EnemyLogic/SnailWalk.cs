using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailWalk : ProtoEnemy, IEnemyMove
{
    [SerializeField] private float moveSpeed = 2f;
    public LayerMask ground;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform fallCheck;
    [SerializeField] private bool haveGround = true;
    [SerializeField] private bool haveWall = false;
    private float groundRadius = 0.20f;
    void Start()
    {
        EnemyManager._onEnemyMoving += Move;
    } 
    protected void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        moveSpeed = -moveSpeed;
    }

    public void Move(EnemyManager enm)
    {
        if (rb2d != null)
        {
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            haveGround = Physics2D.OverlapCircle(fallCheck.position, groundRadius, ground);
            haveWall = Physics2D.OverlapCircle(wallCheck.position, groundRadius, ground);
        }
        if (!haveGround || haveWall)
        {
            Flip();
        }
    }
    public override void EnemyTakingDamage()
    {
        EnemyManager._onEnemyMoving -= Move;
        base.EnemyTakingDamage();
    }
}
