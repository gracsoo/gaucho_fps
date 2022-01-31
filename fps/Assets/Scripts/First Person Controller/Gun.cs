using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public GameObject impactEffect;
    public GameObject bulletHole;

    void Update()
    {
        if(Input.GetButtonDown("Shoot"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            GameObject hole = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            hole.transform.position += hole.transform.forward / 1000;

            Destroy(impact, 2f);
            Destroy(hole, 2f);
        }
    }
}
