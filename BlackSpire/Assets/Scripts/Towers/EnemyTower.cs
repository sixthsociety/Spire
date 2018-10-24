using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class EnemyTower : MonoBehaviour {

    CapsuleCollider range;
    [SerializeField] private float radius = 5f;

    private bool doesAttack = true;
    [SerializeField] private int damage = 3;
    [SerializeField] private float attackRate = 1f; // seconds

    private List<HealthBehaviour> playersInRange = new List<HealthBehaviour>();

    private void Start()
    {
        range = GetComponent<CapsuleCollider>();
        range.radius = radius;
        range.isTrigger = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            HealthBehaviour justEntered = col.GetComponent<HealthBehaviour>();
            playersInRange.Add(justEntered);

            if (playersInRange.Count == 1) 
            {
                InvokeRepeating("DamagePlayer", 0, attackRate);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            HealthBehaviour justExited = col.GetComponent<HealthBehaviour>();
            playersInRange.Remove(justExited);

            if (playersInRange.Count == 0) 
            {
                CancelInvoke("DamagePlayer");
            }
        }
    }

    void DamagePlayer () 
    {
        playersInRange[0].TakeDamage(damage);
    }
}
