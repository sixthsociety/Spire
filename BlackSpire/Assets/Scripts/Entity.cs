using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all entities derive from this class
public class Entity : MonoBehaviour {

    [SerializeField] protected int maxHealth = 100;
    protected int health;

    private void Start()
    {
        health = maxHealth;
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " died...");
    }

    public void Heal(int heal)
    {
        if (health <= maxHealth - heal)
        {
            health += heal;
            Debug.Log("Healed the player " + heal + " health");
        }
        else
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (health - damage <= 0)
        {
            Die();
        }
        else
        {
            health -= damage;
        }
    }
}
