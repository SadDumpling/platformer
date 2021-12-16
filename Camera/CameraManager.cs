using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public delegate void OnCameraMoving(CameraManager camMan);
    static public event OnCameraMoving _onCameraMoving;
    public static float camWidth;
    public static float camHeight;
    [SerializeField] public float EdgeMapMinYPos = 0;
    [SerializeField] public float EdgeMapMinXPos = 0;
    [SerializeField] public float EdgeMapMaxYPos = 0;
    [SerializeField] public float EdgeMapMaxXPos = 0;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
        if (EdgeMapMinYPos == EdgeMapMaxYPos)
        {
            EdgeMapMinYPos = -camHeight;
            EdgeMapMaxYPos = camHeight;
        }
        if (EdgeMapMinXPos == EdgeMapMaxXPos)
        {
            EdgeMapMinXPos = -camWidth;
            EdgeMapMaxXPos = camWidth;
        }
    }
    void Update()
    {
        _onCameraMoving?.Invoke(this);
    }
}
