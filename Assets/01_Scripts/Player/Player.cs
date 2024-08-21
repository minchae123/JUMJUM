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
    [Header("���뺯��")]
    [SerializeField] private EPlayerMode player;

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    private float currentPosX = 0;
    private int dir = 1   ; // ����

    private Rigidbody2D rigid;
    private GroundDetect checkGround;

    private int currentJumpCnt = 0;

    // ======================================

    [Header("���� �÷��̾�")]
    private bool canJump = true;

    // ======================================

    [Header("������ �÷��̾�")]
    [SerializeField] private LayerMask wallLayer;

    private Collider2D playerCol;
    private bool canMove = true;

    // ======================================

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        checkGround = GetComponentInChildren<GroundDetect>();

        playerCol = GetComponent<Collider2D>();

        canMove = true;
        currentPosX = transform.localPosition.x;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (canMove)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.W) && player == EPlayerMode.LEFT)
        {
            LeftJump();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && player == EPlayerMode.RIGHT)
        {
            if (checkGround.IsGround())
            {
                currentJumpCnt = 0;
            }

            if (currentJumpCnt <= 0)
            {
                print("ó������");
                canMove = false;
                RightJump();
            }
            else
            {
                print("�ι�");
                if (IsWall())
                {
                    print("������");
                    RightJump();
                }
            }
        }
    }

    public void LeftJump()
    {
        if (checkGround.IsGround())
        {
            currentJumpCnt = 0;
            canJump = true;
        }

        if (currentJumpCnt < 2 && canJump) // ���� ���� ����
        {
            if (++currentJumpCnt >= 2)
            {
                canJump = false;
            }

            rigid.velocity = Vector2.zero;
            rigid.velocity = new Vector2(.5f * jumpPower, 1.2f * jumpPower);
        }
    }

    public void RightJump()
    {
        rigid.velocity = Vector2.zero;
        rigid.velocity = new Vector2(-dir * jumpPower, 1.1f * jumpPower);
        ++currentJumpCnt;
    }

    private bool IsWall()
    {
        return Physics2D.BoxCast(transform.position, playerCol.bounds.size, 0f, Vector2.left * dir, 0.5f, wallLayer);
    }

    private void Move()
    {
        currentPosX += Time.deltaTime * dir * speed;
        transform.position = new Vector2(currentPosX, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Reverse();
        }
    }

    private void Reverse()
    {
        dir *= -1;
    }
}
