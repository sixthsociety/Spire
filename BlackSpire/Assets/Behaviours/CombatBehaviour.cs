using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// gun, projectile, grenade, melee, airstrike

public class CombatBehaviour : MonoBehaviour
{
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

    void FireBullets()
    {
        Debug.Log("Shoot B");

        //tmp 
        OnKill();
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
}
