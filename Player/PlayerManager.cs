using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void OnPlayerAction(PlayerManager plMn);
    static public event OnPlayerAction _onPlayerAction;

    void FixedUpdate()
    {
        _onPlayerAction?.Invoke(this);
    }
}
