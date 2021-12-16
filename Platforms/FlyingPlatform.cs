using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlatform : MonoBehaviour
{
    [SerializeField] private float stopTime = 2;
    [SerializeField] private float flyTime = 3;
    [SerializeField] private Vector3 moveVector;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(Fly());
        rb2d.isKinematic = true;
    }
    IEnumerator Fly()
    {
        while (true)
        {
            yield return new WaitForSeconds(stopTime);
            FlyingPlatformManager._onPlatformMoving += PlatformMove;
            yield return new WaitForSeconds(flyTime);
            FlyingPlatformManager._onPlatformMoving -= PlatformMove;
            moveVector.y *= -1;
            moveVector.x *= -1;
        }
    }
    public void PlatformMove(FlyingPlatformManager fpm)
    {
        rb2d.MovePosition(transform.position + moveVector * Time.deltaTime);
        Vector2 tempPos = transform.position;
    }
}
