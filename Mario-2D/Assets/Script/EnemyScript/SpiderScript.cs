using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private Rigidbody2D rgb;

    private string corutine_name = "changeMovement";

    private Vector3 Direction = Vector3.down;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rgb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveSpider();
    }
   void moveSpider()
    {
        transform.Translate(Direction * Time.smoothDeltaTime);

    }
    IEnumerator changeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        if (Direction == Vector3.down)
        {
            Direction = Vector3.up;
        }
        else
        {
            Direction = Vector3.down;
        }
       StopCoroutine(corutine_name);
    }
    IEnumerator SpiderDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.Bullet_Tag)
        {
            anim.Play("SpiderDead");
            rgb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(SpiderDead());
        }
    }
}
