using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;

    public GameObject impactSound;
    public GameObject woundSound;
    public GameObject bloodEffect;
    
    void OnCollisionEnter(Collision other)
    {
        // GameObject impact = Instantiate(impactSound, transform.position, Quaternion.identity);

        if(other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("in collision");
            GameObject wound = Instantiate(woundSound, transform.position, Quaternion.identity);
            GameObject blood = Instantiate(bloodEffect, transform.position, Quaternion.identity);
           
            Target target = other.gameObject.transform.GetComponent<Target>();
            if(target != null)
            {
                target.ReduceHealth(damage);
                // Debug.Log("reduced health");
            }
        }
        else
        {
            GameObject impact = Instantiate(impactSound, transform.position, Quaternion.identity);

        }
        // Debug.Log("in collision");
        // Destroy(impact);
        Destroy(gameObject);
    }
}
