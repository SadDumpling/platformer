using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxAndCameraMaster : MonoBehaviour
{
    private Vector3 tempPos;
    private float EdgeMapMinYPos;
    private float EdgeMapMinXPos;
    private float EdgeMapMaxYPos;
    private float EdgeMapMaxXPos;
    [SerializeField] private int paralaxMultiplier = 1;
    private float camWidth;
    private float camHeight;
    private float moveDirection;
    [SerializeField] private float cameraIndent = 3;
    [SerializeField] private float cameraSkid = 0.2f;
    void Start()
    {
        tempPos = transform.position;
        var tempCameraManager = FindObjectOfType<CameraManager>();
        CameraManager._onCameraMoving += Move;
        camWidth = CameraManager.camWidth;
        camHeight = CameraManager.camHeight;
        EdgeMapMinYPos = tempCameraManager.EdgeMapMinYPos;
        EdgeMapMinXPos = tempCameraManager.EdgeMapMinXPos;
        EdgeMapMaxYPos = tempCameraManager.EdgeMapMaxYPos;
        EdgeMapMaxXPos = tempCameraManager.EdgeMapMaxXPos;
        tempCameraManager = null;
    }
    private void CameraInLevelMaker()
    {
        if (tempPos.x < EdgeMapMinXPos + camWidth)
            tempPos.x = EdgeMapMinXPos + camWidth;
        else if (tempPos.x > EdgeMapMaxXPos - camWidth)
            tempPos.x = EdgeMapMaxXPos - camWidth;
        if (tempPos.y < EdgeMapMinYPos + camHeight)
            tempPos.y = EdgeMapMinYPos + camHeight;
        else if (tempPos.y > EdgeMapMaxYPos - camHeight)
            tempPos.y = EdgeMapMaxYPos - camHeight;
    }
    private void CameraSlip()
    {
        // not neaded now
    }
    private void Paralax()
    {
        tempPos.x /= paralaxMultiplier;
        tempPos.y /= paralaxMultiplier;
    }
    private void PlayerPosition()
    {
        tempPos.x = PlayerMove.playerPosition.x;
        tempPos.y = PlayerMove.playerPosition.y;
        // only for CameraSlip
        //moveDirection = PlayerMove.MoveInput;
    }
    public void Move (CameraManager camMan)
    {
        PlayerPosition();
        CameraSlip();
        CameraInLevelMaker();
        Paralax();
        transform.position = tempPos;
    }
}
