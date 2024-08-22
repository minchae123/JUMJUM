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
    [Header("공통변수")]
    [SerializeField] private EPlayerMode player;

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    private float currentPosX = 0;
    private int dir = 1   ; // 방향

    private bool isReverse = true;
    private bool isGround;
    private bool isWall;

    private Rigidbody2D rigid;
    private GroundDetect checkGround;

    private Transform visual;
    private SpriteRenderer visualSr;

    private int currentJumpCnt = 0;


    // ======================================

    [Header("왼쪽 플레이어")]
    private bool canJump = true;

    // ======================================

    [Header("오른쪽 플레이어")]
    [SerializeField] private LayerMask wallLayer;

    private Collider2D playerCol;

    // ======================================

    private void Awake()
    {
        visual = transform.Find("Visual");

        rigid = GetComponent<Rigidbody2D>();
        checkGround = GetComponentInChildren<GroundDetect>();
        playerCol = GetComponent<Collider2D>();
        visualSr = visual.GetComponent<SpriteRenderer>();

        currentPosX = transform.localPosition.x;
    }

    private void Start()
    {

    }

    private void Update()
    {
        //print(isWall);
        Move();
        CheckCurrentState();

        if (Input.GetKeyDown(KeyCode.W) && player == EPlayerMode.LEFT)
        {
            LeftJump();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && player == EPlayerMode.RIGHT)
        {
            RightJump();
        }
    }

    public void CheckCurrentState()
    {
        ECollisionFlags collision = checkGround.GetCollisionState();
        //print(collision);

        if (collision.HasFlag(ECollisionFlags.Wall))
        {
            Reverse();
            isWall = true;
        }
        else
        {
            isWall = false;
        }

        if (collision.HasFlag(ECollisionFlags.Ground))
        {
            isGround = true;

            currentJumpCnt = 0;
            canJump = true;
        }
        else
        {
            isGround = false;
        }
    }

    public void LeftJump()
    {
        if (currentJumpCnt < 2 && canJump) // 더블 점프 지원
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
        if (currentJumpCnt > 0)
        {
            if (isWall) // 벽ㅇㅔ 닿았을 때만
            {
                print("wall");
                Jump();
            }
        }
        else
        {
            Jump();
        }

        ++currentJumpCnt;
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

    public void Reverse()
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
