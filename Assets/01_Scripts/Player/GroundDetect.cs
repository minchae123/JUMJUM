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
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        player = GetComponentInParent<Player>();
    }

    public ECollisionFlags GetCollisionState(int dir)
    {
        ECollisionFlags flags = ECollisionFlags.None;

        RaycastHit2D hitGround = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.5f, collisionLayer);
        RaycastHit2D hitWall = Physics2D.BoxCast(col.bounds.center + Vector3.up * 0.1f, col.bounds.size, 0f, Vector2.right * dir, 0.5f, collisionLayer);

        if (hitGround.collider != null) flags |= ECollisionFlags.Ground;
        if(hitWall.collider !=null) flags |= ECollisionFlags.Wall;

        //Collider2D[] collisions = Physics2D.OverlapBoxAll(col.bounds.center, col.bounds.size * 1.1f, 0, collisionLayer);
        /*foreach (Collider2D collision in collisions)
        {
            if (collision.CompareTag("Wall"))
            {
                flags |= ECollisionFlags.Wall;
            }
            else if (collision.CompareTag("Ground"))
            {
                flags |= ECollisionFlags.Ground;
            }
        }*/

        print(flags);

        return flags;
    }
}
