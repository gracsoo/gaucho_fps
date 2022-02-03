using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    int damage = 10;

    public GameObject impactEffect;
    public GameObject bulletHole;


    void OnCollisionEnter(Collision other)
    {
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);

        if(other.gameObject.CompareTag("Enemy"))
        {
            Target target = other.gameObject.transform.GetComponent<Target>();
            target.ReduceHealth(damage);
        }
        else
        {
            GameObject hole = Instantiate(bulletHole, transform.position, Quaternion.LookRotation(other.contacts[0].normal));
            hole.transform.position += hole.transform.forward / 1000;
            Destroy(hole, 2f);
        }
        Destroy(impact, 2f);
        Destroy(gameObject);
    }
}
