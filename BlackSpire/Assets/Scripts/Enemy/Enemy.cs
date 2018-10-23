using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    [SerializeField] protected new int maxHealth;

    private void Start()
    {
        health = maxHealth;
    }
    
}
