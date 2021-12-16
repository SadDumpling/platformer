using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleWalk : SnailWalk
{
    public override void EnemyTakingDamage()
    {
        EnemyManager._onEnemyMoving -= Move;
    }
}
