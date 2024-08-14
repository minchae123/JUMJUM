using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    NONE = 0,
    LEFT = 1,
    RIGHT = 2
}

public class Player : MonoBehaviour
{
    [Header("공통변수")]
    [SerializeField] private PlayerMode player;

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    private Rigidbody2D rigid;
    private GroundDetect checkGround;
    public bool IsGround => checkGround.IsGround;

    [Header("왼쪽 플레이어")]
    private int currentJumpCnt = 0;

    [Header("오른쪽 플레이어")]
    [SerializeField] private LayerMask wallLayer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        checkGround = GetComponentInChildren<GroundDetect>();
    }

    private void Start()
    {
        Move(Vector2.right); // 시작할 때 오른쪽으로 이동
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && player == PlayerMode.LEFT)
        {
            LeftJump();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && player == PlayerMode.RIGHT)
        {
            RightJump();
        }
    }

    public void LeftJump()
    {
        if (currentJumpCnt < 2) // 더블 점프 지원
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            ++currentJumpCnt;
        }
    }

    public void RightJump()
    {
        // 오른쪽 플레이어의 점프 로직을 여기에 추가
    }

    private void Move(Vector2 direction)
    {
        rigid.velocity = direction * speed; // 속도를 설정
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        // 현재 속도가 0인 경우에는 기본 속도로 설정
        float x = Mathf.Sign(rigid.velocity.x) == 0 ? 1 : -Mathf.Sign(rigid.velocity.x);
        Move(Vector2.right * x); // 방향을 반전하여 속도를 설정
    }
}
