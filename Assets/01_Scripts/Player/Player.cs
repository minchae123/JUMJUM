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
    [Header("���뺯��")]
    [SerializeField] private PlayerMode player;

    [SerializeField] private float speed;
    private float currentPosX = 0;
    private float dir = 1   ; // ����
    [SerializeField] private float jumpPower;

    private Rigidbody2D rigid;
    private GroundDetect checkGround;
    public bool IsGround => checkGround.IsGround;

    [Header("���� �÷��̾�")]
    private int currentJumpCnt = 0;

    [Header("������ �÷��̾�")]
    [SerializeField] private LayerMask wallLayer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        checkGround = GetComponentInChildren<GroundDetect>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        Move();

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
        if (currentJumpCnt < 2) // ���� ���� ����
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            ++currentJumpCnt;
        }
    }

    public void RightJump()
    {
        // ������ �÷��̾��� ���� ������ ���⿡ �߰�
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
