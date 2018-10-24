using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// damage, damage over time
public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    protected int health;

    [SerializeField] protected float respawnDelay = 3f;

    private void Start()
    {
        health = maxHealth;
        Die();
    }

    public virtual void Respawn(Vector3 respawnPoint)
    {
        Debug.Log(gameObject.name + " respawned...");

        transform.position = respawnPoint;
        health = maxHealth;
        gameObject.SetActive(true);
    }

    public delegate void DeathEventHandler(object health, EventArgs args);
    public event DeathEventHandler Died;

    public virtual void OnDie() 
    {
        if( Died!= null)
        {
            Died(this, EventArgs.Empty);
        } 
    }
   
    public virtual void Die()
    {
        Debug.Log(gameObject.name + " died...");

        OnDie();

        gameObject.SetActive(false);
    }

    public void Heal(int heal)
    {
        if (health <= maxHealth - heal)
        {
            health += heal;
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
