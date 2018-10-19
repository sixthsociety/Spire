using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
public class Player : MonoBehaviour {

    PlayerMove playerMove;
    PlayerWeapon playerWeapon;

    private bool inBase;

    private int health;
    [SerializeField] private int maxHealth = 100;

    void Start () 
    {
        playerMove = GetComponent<PlayerMove>();
        playerWeapon = GetComponentInChildren<PlayerWeapon>();

        health = maxHealth;
    }

    public void SetBase (bool _inBase) 
    {
        inBase = _inBase;
    }

    void Die () 
    {
        Debug.Log("YOu died...");
    }

    public void Heal (int heal) 
    {
        if (health <= maxHealth - heal)
        {
            health += heal;
            Debug.Log("Healed the player " + heal + " health");
        } else 
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
