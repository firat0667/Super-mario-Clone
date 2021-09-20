
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooT : MonoBehaviour
{
    public GameObject fireBullet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }
    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.lossyScale.x;
        }
    }
}
