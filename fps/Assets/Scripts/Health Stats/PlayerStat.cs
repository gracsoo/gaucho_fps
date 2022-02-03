using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : CharacterStat
{
    public override void Die()
    {
        // base.Die();

        
        Debug.Log("inside player override die");

        gameObject.GetComponent<PlayerMovement>().isDead = true;
        IncreaseHealth(100);

    }
}
