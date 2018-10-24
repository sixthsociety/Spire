using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// gun, projectile, grenade, melee, airstrike

public class CombatBehaviour : MonoBehaviour {

    AttackStats stats;

    public struct AttackStats
    {
        public int baseDamage;
        public float attackTime;
    }

	void OnEnable () {
		
	}
	

	public void SetAttack (int baseDamage, float attackTime) {
        stats.baseDamage = baseDamage;
        stats.attackTime = attackTime;
	}
}
