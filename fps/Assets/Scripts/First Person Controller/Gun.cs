using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    public int bulletCount;
    public int bulletSpeed = 10;
    public int damage = 10;
    public float range = 100f;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public float fireRate;
    private float nextFire = 0.0F;

    bool shooting, readyToShoot;

    public Camera fpsCam;
    public Transform attackPoint;
    public GameObject bullet;
    public GameObject prefabSound;
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    void Start()
    {
        bulletCount = magazineSize;
        // readyToShoot = true;
    }
    
    void Update()
    {
        if(allowButtonHold) 
        {
            shooting = Input.GetKey(KeyCode.X);
            fireRate = 0.05f;
        }
        else 
        {
            shooting = Input.GetKeyDown(KeyCode.X);
            fireRate = 0.1f;
        }
        
        if(shooting && bulletCount > 0 && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
            bulletCount--;
        }

        if(ammunitionDisplay != null)
            ammunitionDisplay.SetText(((byte)bulletCount)/bulletsPerTap + " / " + magazineSize/bulletsPerTap);
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Debug.Log(hit.transform.name);
            GameObject flash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
            
            GameObject firingBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //Quaternion.Euler(90,0,0)
            GameObject m_Sound = Instantiate(prefabSound, attackPoint.position, Quaternion.identity);

            Vector3 bulletDirection = hit.point - attackPoint.position;
            firingBullet.GetComponent<Rigidbody>().velocity = bulletDirection * bulletSpeed;

            // Target target = hit.transform.GetComponent<Target>();
            // if(target != null)
            // {
            //     target.ReduceHealth(damage);


            //     GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            //     Destroy(impact, 2f);

            // }
            // else if(target == null)
            // {
            //     GameObject hole = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            //     hole.transform.position += hole.transform.forward / 1000;
            //     Destroy(hole, 2f);
            // }
            
        }
    }

    public void Reload(int amount)
    {
        if(bulletCount+amount <= magazineSize)
            bulletCount += amount;
        else
            bulletCount = magazineSize;
    }
}
