using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Flags]
public enum ECollisionFlags
{
    None = 0,
    Ground = 1,
    Wall = 1 << 1
}

public class GroundDetect : MonoBehaviour
{
    private Player player;

    [SerializeField] private LayerMask collisionLayer;
    private Collider2D groundCol;

    private void Awake()
    {
        groundCol = GetComponent<Collider2D>();
        player = GetComponentInParent<Player>();
    }

    public ECollisionFlags GetCollisionState()
    {
        ECollisionFlags flags = ECollisionFlags.None;

        Collider2D[] collisions = Physics2D.OverlapBoxAll(groundCol.bounds.center, groundCol.bounds.size, 0, collisionLayer);
        foreach(Collider2D collision in collisions)
        {
            if (collision.CompareTag("Wall"))
            {
                flags |= ECollisionFlags.Wall;
            }
            else if (collision.CompareTag("Ground"))
            {
                flags |= ECollisionFlags.Ground;
            }
        }

        return flags;
    }
}
