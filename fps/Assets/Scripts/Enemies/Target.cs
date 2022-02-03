using UnityEngine;

public class Target : MonoBehaviour
{
    // public float health = 50f;
    
    public CharacterStat myStats;
    // public bool isDead
    // {
    //     get{
    //         return myStats.currentHealth == 0;
    //     }
    // }

    
    void Start()
    {
        myStats = GetComponent<CharacterStat>();
    }
    
    public void ReduceHealth(int damage)
    {   
        myStats.TakeDamage(damage);

        // if(isDead)
    }

    // public void TakeDamage(float amount)
    // {
    //     health -= amount;
    //     if(health <= 0f)
    //     {
    //         Die();
    //     }
    // }

    // void Die()
    // {
    //     Destroy(gameObject, 2f);
    // }
}
