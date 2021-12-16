using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoEnemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb2d;
    protected bool enemyTakeDamage = false;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        anim.speed = 1;
    }
    public virtual void EnemyTakingDamage()
    {
        enemyTakeDamage = true;
        anim.SetBool("hit", true);
        gameObject.layer = 11;
        Destroy(rb2d);
        StartCoroutine(EnemyDie());
    }
    IEnumerator EnemyDie()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
