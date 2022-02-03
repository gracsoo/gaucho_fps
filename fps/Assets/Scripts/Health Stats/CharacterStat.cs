using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public Stat damage;

    // 1st int = maxHealth, 2nd int = currentHealth
    public event System.Action<int,int> OnHealthChanged;


    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void IncreaseHealth(int health)
    {
        if (health+currentHealth <= maxHealth)
            currentHealth += health;
        else
            currentHealth = maxHealth;
            
        if(OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes" + damage + " damage");

        if(OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    }


    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died");
    }
}
