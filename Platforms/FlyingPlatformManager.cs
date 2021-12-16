using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlatformManager : MonoBehaviour
{
    public delegate void OnPlatformMoving(FlyingPlatformManager fpm);
    static public event OnPlatformMoving _onPlatformMoving;
    void FixedUpdate()
    {
        _onPlatformMoving?.Invoke(this);
    }
}
