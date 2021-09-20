using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rgb;
    private Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundlayer;
    private bool isGrounded;
    private bool jumped;


    private float jumpPower = 12f;


    private void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        CheckIfGrounded();
        playerJump();

    } 
    private void FixedUpdate()
    {
        PlayerWalk();
    }
    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            rgb.velocity = new Vector2(speed, rgb.velocity.y);
            ChanceDirection(1);
        }
        else if (h < 0)
        {
            rgb.velocity = new Vector2(-speed, rgb.velocity.y);
            ChanceDirection(-1);
        }
        else
        {
            rgb.velocity = new Vector2(0, rgb.velocity.y);
        }
        anim.SetInteger("Speed", Mathf.Abs((int)rgb.velocity.x));
    }
    void ChanceDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      /*  if (collision.gameObject.tag == "Ground")
        {
            print("Colllision ground ");
        } */

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down,0.1f,groundlayer);
        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }
    void playerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                rgb.velocity = new Vector2(rgb.velocity.x, jumpPower);

                anim.SetBool("Jump", true);
            }
        }
    }
}
