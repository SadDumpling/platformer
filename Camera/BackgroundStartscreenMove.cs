using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStartscreenMove : MonoBehaviour
{
    void Update()
    {
        transform.Translate(new Vector3(-0.2f, 0, 0) * Time.deltaTime);
        Vector3 pos = transform.position;
        if (pos.x <= -50)
            pos.x = 0;
        transform.position = pos;
    }
}
