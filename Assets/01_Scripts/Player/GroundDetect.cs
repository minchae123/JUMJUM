using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    private Player player;

    [SerializeField] private LayerMask groundLayer;
    
    private Collider2D groundCol;
    private float raycastValue = 0.5f;

    private void Awake()
    {
        groundCol = GetComponent<Collider2D>();
        player = GetComponentInParent<Player>();
    }

    public bool IsGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(groundCol.bounds.center, groundCol.bounds.size, 0f, Vector2.down, raycastValue, groundLayer);
        return hit.collider != null;
    }
}
