using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public delegate void OnEnemyMoving(EnemyManager enm);
    static public event OnEnemyMoving _onEnemyMoving;
    void FixedUpdate()
    {
        _onEnemyMoving?.Invoke(this);
    }
}
