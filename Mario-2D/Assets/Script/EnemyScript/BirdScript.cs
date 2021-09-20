using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private Rigidbody2D rgb;
    private Animator anim;

    private Vector3 moveDirection = Vector3.left;
    private Vector3 originPos;
    private Vector3 movePos;

    public GameObject birdEgg;
    public LayerMask PlayerLayer;
    
    private float speed = 2f;

    private bool canMove;
    private bool attacked;
    private void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        originPos = transform.position;
        originPos.x += 6f;

        movePos = transform.position;
        movePos.x -= 6f;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTheBird();
        DropTheEgg();
    }
    void MoveTheBird()
    {
        if (canMove)
        {
            transform.Translate(moveDirection *speed* Time.smoothDeltaTime);
            if (transform.position.x >= originPos.x)
            {
                moveDirection = Vector3.left;

                ChanceDirection(0.5f);

            }
            else if (transform.position.x <= movePos.x)
            {
                moveDirection = Vector3.right;

                ChanceDirection(-0.5f);

            }
        }
    }
    void ChanceDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;


    }
    void DropTheEgg()
    {
        if (!attacked)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, PlayerLayer)){

                Instantiate(birdEgg, new Vector3(transform.position.x,
               transform.position.y - 1f,transform.position.z),Quaternion.identity);
                attacked = true;
                anim.Play("FlyBird");
            }
        }
    }
    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.Bullet_Tag)
        {
            anim.Play("BirdDead");
            GetComponent<BoxCollider2D>().isTrigger = true;
            rgb.bodyType = RigidbodyType2D.Dynamic;
            canMove = false;
            StartCoroutine(BirdDead());
        }
    }
}
