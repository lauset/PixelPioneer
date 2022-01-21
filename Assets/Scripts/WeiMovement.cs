using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeiMovement : MonoBehaviour
{
    // 刚体
    public Rigidbody2D ri;
    // 圆形碰撞体
    public Collider2D coll;
    // 当前动作
    public Animator anim;
    public Transform groundCheck;
    public LayerMask ground;
    // 弹跳次数
    public int jumpCount;
    // 速度与弹跳力度
    public float PlayerSpeed, JumpForce;
    public bool isGround, isJump, jumpPressed, isSlide;
    // 蔚蓝部分
    public bool onGround, onWall;
    public float collisionRadius = 0.2f;
    // 滑落速度
    public float SlideSpeed = 0.2f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;


    void Start()
    {

    }

    void Update()
    {
        //JumpPlayer();
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

    void WallSlide()
    {
        ri.velocity = new Vector2(ri.velocity.x, -SlideSpeed);
    }

    void FixedUpdate()
    {
        //MovePlayer();
        //ChangeAnim();

        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        // 蔚蓝
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, ground);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, ground) ||
            Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, ground);

        // 滑墙
        if (onWall && !onGround)
        {
            isSlide = true;
            WallSlide();
        }
        else
        {
            isSlide = false;
        }

        GroundMove();
        JumpTwo();
        SwitchAnim();
    }

    void GroundMove()
    {
        float hm = Input.GetAxisRaw("Horizontal");
        float hv = Input.GetAxis("Vertical");

        // W爬墙
        if (onWall && Input.GetKey(KeyCode.W))
        {
            ri.velocity = new Vector2(ri.velocity.x, hv * PlayerSpeed * 0.5f);
        }
        // 地面或空中移动
        if (!isSlide)
        {
            ri.velocity = new Vector2(hm * PlayerSpeed, ri.velocity.y);
        }
        // 角色转向
        if (hm != 0)
        {
            transform.localScale = new Vector3(hm, 1, 1);
        }
    }

    void JumpTwo()
    {
        // 重置段跳次数
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        // 在地面并按跳跃键
        if (jumpPressed && isGround)
        {
            isJump = true;
            ri.velocity = new Vector2(ri.velocity.x, 0);
            ri.velocity += Vector2.up * JumpForce;
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && !isGround && !onWall)
        {
            ri.velocity = new Vector2(ri.velocity.x, JumpForce);
            ri.velocity += Vector2.up * JumpForce;
            jumpCount--;
            jumpPressed = false;
        }
    }

    void SwitchAnim()
    {
        anim.SetFloat("running", Mathf.Abs(ri.velocity.x));
        if (isGround)
        {
            anim.SetBool("falling", false);
        }
        else if (ri.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (ri.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }
}
