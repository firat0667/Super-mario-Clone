using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 10f;
    private Animator anim;
    private bool canMOve;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        canMOve = true;
        StartCoroutine(DisableBullet(5f));
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move()
    {
        if (canMOve)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
       
    }
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
           speed = value;
        }
    }
    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag==MyTags.Beetle_Tag || collision.gameObject.tag == MyTags.Snail_Tag
            || collision.gameObject.tag == MyTags.Spider_Tag)
        {
            canMOve = false;
            anim.Play("Explode");
            StartCoroutine(DisableBullet(0.2f));
        }
    }
}
