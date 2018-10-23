using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all entities derive from this class
public class Entity : MonoBehaviour {

    [SerializeField] public int maxHealth = 100;
    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    void Die()
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
