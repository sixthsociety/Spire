using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotManager : MonoBehaviour {

	public GameObject target;
	public float attackRange;
	public float attackTimer;
	public float maxAttackTimer;
	public string[] enemyTags;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		
		agent = GetComponent<NavMeshAgent>();
		attackTimer = maxAttackTimer;
		agent.stoppingDistance = attackRange;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (target != null){
			
			if (Vector3.Distance(transform.position - transform.localScale, target.transform.position - target.transform.localScale) <= attackRange){

				agent.isStopped = true;
				AttackTarget();
			}else{
				
				ReachTarget();
			}

		}else{

			FindTarget();
		}

		if (attackTimer > 0f){

			attackTimer -= Time.deltaTime;
		}
	}

	public void FindTarget(){

		for (int i = 0; i < enemyTags.Length; i++){

			Debug.Log("Finding a new Target");
			GameObject[] targets = GameObject.FindGameObjectsWithTag(enemyTags[i]);
			int tempIndex = -1;
			float minDist = -1f;
			for (int j = 0; j < targets.Length; j++){

				float thisDist = Vector3.Distance(transform.position, targets[j].transform.position);

				if (thisDist < minDist){

					minDist = thisDist;
					tempIndex = j;
				}else if(minDist <= -1f){

					minDist = thisDist;
					tempIndex = j;
				}
			}
			if (tempIndex >= 0){

				target = targets[tempIndex];
				break;
			}
		}
	}

	public void ReachTarget(){

		Debug.Log("Reaching the Target");
		agent.isStopped = false;
		agent.destination = target.transform.position;
	}

	public void AttackTarget(){

		if(target.CompareTag("Enemy") || target.CompareTag("Tower")){

			if (attackTimer <= 0f){

				Debug.Log("Attacking the Target");
				target.GetComponent<EnemyHealth>().health--;
				attackTimer = maxAttackTimer;
			}
		}
	}
}
