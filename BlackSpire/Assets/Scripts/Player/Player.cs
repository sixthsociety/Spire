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
<<<<<<< HEAD
            health -= damage;
        }
    }

    [SerializeField] private Weapon toCall;
    void WeaponRequest () 
    {
        //call ui manager

        //get input from the manager as to which gun to use

        Debug.Log("Calling new weapon...");
        //Get the gamemanager to drop a new weapon box in the base
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && inBase)
        {
            WeaponRequest();
        }
    }
}
=======
            health -= damage;
        }
    }
}
>>>>>>> 9e3fae69f95042eeb8f64a59a7cb4b96adde2453
