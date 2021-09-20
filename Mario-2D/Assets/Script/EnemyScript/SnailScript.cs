using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D rgb;
    private Animator ainm;
    private bool canMove;
    private bool stunned;
    public bool moveLeft;
    public LayerMask PlayerLayer;

    public Transform left_collison, right_collison, top_collison, down_collision;
    private Vector3 left_collison_Pos, right_collison_Pos;

    private void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        ainm = GetComponent<Animator>();
        left_collison_Pos = left_collison.position;
        right_collison_Pos = right_collison.position;
    }
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                rgb.velocity = new Vector2(-moveSpeed, rgb.velocity.y);
            }
            else
            {
                rgb.velocity = new Vector2(moveSpeed, rgb.velocity.y);
            }
        }
    
        checkCollison();
    }
    void checkCollison()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(left_collison.position, Vector2.left, 0.1f,PlayerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_collison.position, Vector2.right, 0.1f, PlayerLayer);
        Collider2D topHit = Physics2D.OverlapCircle(top_collison.position, 0.2f, PlayerLayer);
        if (topHit!=null)
        {
            if (topHit.gameObject.tag==MyTags.Player_Tag)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    canMove = false;
                    rgb.velocity = new Vector2(0, 0);
                    Debug.Log("deðgi");
                    ainm.Play("Stunned");
                    stunned = true;

                    //BEETLE CODE HERE
                    if (gameObject.tag == MyTags.Beetle_Tag)
                    {
                        ainm.Play("BStun");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }
        if (leftHit)
        {
            if (leftHit.collider.gameObject.tag ==MyTags.Player_Tag)
            {
                if (!stunned)
                {
                    //APPLY DAMAGE  TO PLAYER
                    print("damageleft");
                }
                else
                {
                    if(tag != MyTags.Beetle_Tag)
                    {
                        rgb.velocity = new Vector2(15f, rgb.velocity.y);
                    }
                    
                }
            }
        }
        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == MyTags.Player_Tag)
            {
                if (!stunned)
                {
                    //APPLY DAMAGE  TO PLAYER
                    print("damageright");
                }
                else
                {
                    if (tag != MyTags.Beetle_Tag)
                    {
                        rgb.velocity = new Vector2(-15f, rgb.velocity.y);
                    }
                }
            }
        }
        if (!Physics2D.Raycast(down_collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }
    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;
        if (moveLeft)
        {
           
            tempScale.x = Mathf.Abs(tempScale.x);
            left_collison.position = left_collison_Pos;
            right_collison.position = right_collison_Pos;
        }
        else
        {
            
            tempScale.x = -Mathf.Abs(tempScale.x);
            left_collison.position = right_collison_Pos;
            right_collison.position = left_collison_Pos;
        }
        transform.localScale = tempScale;
    }
    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.Bullet_Tag)
        {
            if (tag == MyTags.Beetle_Tag)
            {
                ainm.Play("BStun");
                rgb.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.4f));
            }
            if (tag == MyTags.Snail_Tag)
            {
                if (!stunned)
                {
                    ainm.Play("Stunned");
                    stunned = true;
                    canMove = false;
                    rgb.velocity = new Vector2(0, 0);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
  
