using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    // public Gun myGun;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("found player collider");
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("found player");
            other.GetComponent<Gun>().Reload(25);
            Destroy(gameObject);
        }
    }
}
