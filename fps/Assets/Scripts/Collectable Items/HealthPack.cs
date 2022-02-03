using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    // public GameObject player;
    // private CharacterStat myStats;
    
    void Start()
    {
        // myStats = player.GetComponent<CharacterStat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("found player collider");
        if(other.gameObject.CompareTag("Player"))
        {
            // myStats.IncreaseHealth(10);
            other.GetComponent<CharacterStat>().IncreaseHealth(50);
            Destroy(gameObject);
        }
    }
}
