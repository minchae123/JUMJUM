using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerMode
{
    NONE = 0,
    LEFT = 1,
    RIGHT = 2
}

public class Player : MonoBehaviour
{
    [SerializeField] private EPlayerMode player; // none XXX

    [SerializeField] private float speed; // 10
    [SerializeField] private float jumpPower; // 15

    private float currentPosX = 0;
    private int dir = 1   ; // direction

    private bool canJump = true; // only left player
    private bool isReverse = true; //
    private bool isGround; // ground check
    private bool isWall; // wall check

    private Rigidbody2D rigid;
    private GroundDetect checkGround;

    private Transform visual;
    private SpriteRenderer visualSr;

    private int currentJumpCnt = 0;

    // ======================================

    private void Awake()
    {
        visual = transform.Find("Visual");

        rigid = GetComponent<Rigidbody2D>();
        checkGround = GetComponentInChildren<GroundDetect>();
        visualSr = visual.GetComponent<SpriteRenderer>();

        if(player == EPlayerMode.NONE)
        {
            Debug.LogWarning("PlayerMode Set NONE");
        }
    }

    private void Start()
    {
        currentPosX = transform.localPosition.x;
    }

    private void FixedUpdate()
    {
        CheckCurrentState();
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.W) && player == EPlayerMode.LEFT)
        {
            LeftJump();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && player == EPlayerMode.RIGHT)
        {
            RightJump();
        }
    }

    private IEnumerator WallWaitCor()
    {
        yield return new WaitForSeconds(.1f);
        isWall = false;
    }

    private void CheckCurrentState()
    {
        ECollisionFlags collision = checkGround.GetCollisionState();
        if (collision.HasFlag(ECollisionFlags.Wall))
        {
            Reverse();
            isWall = true;
        }
        else
        {
            if (isWall)
            {
                StartCoroutine(WallWaitCor());
            }
        }

        if (collision.HasFlag(ECollisionFlags.Ground))
        {
            isGround = true;
        }
        else
        {
            if(isGround) isGround = false;
        }
    }

    public void LeftJump()
    {
        if(isGround)
        {
            currentJumpCnt = 0;
            canJump = true;
        }

        if (currentJumpCnt < 2 && canJump) // 더블 점프
        {
            if (++currentJumpCnt >= 2)
            {
                canJump = false;
            }

            Jump();
        }
    }

    public void RightJump()
    {
        if (isGround)
        {
            currentJumpCnt = 0;
        }

        ++currentJumpCnt;
        if ((currentJumpCnt > 1 && isWall) || currentJumpCnt <= 1)
        {
            Jump();
        }
    }
    
    private void Jump()
    {
        rigid.velocity = Vector2.zero;
        rigid.velocity = new Vector2(.5f * jumpPower, 1.2f * jumpPower);
    }

    private void Move()
    {
        currentPosX += Time.deltaTime * dir * speed;
        transform.position = new Vector2(currentPosX, transform.position.y);
    }

    private void Reverse()
    {
        if (!isReverse) return;

        dir *= -1;
        visualSr.flipX = dir < 0;

        StartCoroutine(ReverseWaitCor());
    }

    private IEnumerator ReverseWaitCor()
    {
        isReverse = false;
        yield return new WaitForSeconds(.1f);
        isReverse = true;
    }
}
