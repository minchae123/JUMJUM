using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    private bool isGround = false;
    public bool IsGround => isGround;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
}
