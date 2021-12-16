using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private bool playerDie = false;
    private PlayerGroundcheck groundcheck;
    private Rigidbody2D rb2d;
    private PlayerJump pJump;
    public bool PlayerDie {
        get
        {
            return playerDie;
        }
    }
    void Start()
    {
        groundcheck = GetComponent<PlayerGroundcheck>();
        rb2d = GetComponent<Rigidbody2D>();
        pJump = GetComponent<PlayerJump>();
    }
    private void EnemyCollision(GameObject enemy)
    {
        if (this.transform.GetChild(0).gameObject.transform.position.y > enemy.transform.position.y)
        {
            EnemyMustDie(enemy);
        }
        else
        {
            PlayerMustDie();
        }
    }
    private void PlayerMustDie()
    {
        gameObject.layer = 11;
        rb2d.AddForce(new Vector2(0, pJump.JumpPower * 1.5f));
        playerDie = true;
    }
    private void EnemyMustDie(GameObject enemy)
    {
        float tempJumpForce = 1;
        tempJumpForce = pJump.JumpButtonDownBool == true ? 2.2f : 1.3f;
        rb2d.AddForce(new Vector2(0, pJump.JumpPower * tempJumpForce));
        enemy.transform.GetComponent<ProtoEnemy>().EnemyTakingDamage();
    }
    private void FruitCollision()
    {

    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject target = coll.gameObject;
        if (target.GetComponent<ProtoEnemy>() != null)
        {
            EnemyCollision(target);
        }
        if(target.GetComponent<ProtoTrap>() != null)
        {
            PlayerMustDie();
        }
    }
}
