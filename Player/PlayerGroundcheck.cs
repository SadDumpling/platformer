using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundcheck : MonoBehaviour
{
    [SerializeField] public Transform groundCheck1;
    [SerializeField] public Transform groundCheck2;
    public LayerMask ground;
    [SerializeField] private float groundRadius = 0.05f;
    private bool isGrounded = false;

    public bool GroundCheck()
    {
        return isGrounded = (Physics2D.OverlapCircle(groundCheck1.position, groundRadius, ground) || Physics2D.OverlapCircle(groundCheck2.position, groundRadius, ground));
    }
}
