using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// gun, projectile, grenade, melee, airstrike

public class CombatBehaviour : MonoBehaviour
{
    //ray, impact, range
    public enum AttackType { Bullets, Missile, Grenade, Melee }

    AttackStats stats;

    public struct AttackStats
    {
        public int baseDamage;
        public float attackTime;
        public AttackType type;
    }

    void OnEnable()
    {

    }

    public void SetAttack(int baseDamage, float attackTime)
    {
        stats.baseDamage = baseDamage;
        stats.attackTime = attackTime;
    }

	public void SetAttack (int baseDamage, float attackTime, AttackType type) {
        stats.baseDamage = baseDamage;
        stats.attackTime = attackTime;
        stats.type = type;
	}

    public void Attack()
    {
        switch (stats.type)
        {
            case AttackType.Bullets: FireBullets(); break;
            case AttackType.Missile: LaunchMissile(); break;
            case AttackType.Grenade: ThrowGrenade(); break;
            case AttackType.Melee: Melee(); break;
        }
    }

    float bulletRange = Mathf.Infinity;
    public Vector3 spawnOffset;
    void FireBullets()
    {
        Debug.Log("Shoot B");

        Vector3 p = this.transform.TransformPoint(spawnOffset);

        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(p, transform.forward, out hitInfo, bulletRange, -1, QueryTriggerInteraction.Ignore);
        if (isHit)
        {
            HealthBehaviour health = hitInfo.collider.GetComponent<HealthBehaviour>();
            if (health != null) Damage(health);
        }

        // TODO add kill tracking and only call OnKill() when player kills something
        //OnKill();
    }
    void LaunchMissile()
    {
        Debug.Log("Launch M");
    }
    void ThrowGrenade()
    {
        Debug.Log("Throw G");
    }
    void Melee () 
    {
        Debug.Log("Melee attacked");
    }

    void Damage(HealthBehaviour healthBehaviour)
    {
        healthBehaviour.TakeDamage(1);
    }

    public delegate void ObjectiveEventHandler(object source, EventArgs e);
    public event ObjectiveEventHandler E_OnKill;

    public void OnKill()
    {
        if (E_OnKill != null)
        {
            E_OnKill(this, EventArgs.Empty);
        }
    }
}
