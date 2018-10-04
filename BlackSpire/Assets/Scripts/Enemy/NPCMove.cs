using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour {
    [SerializeField]
    Transform _destination;

    UnityEngine.AI.NavMeshAgent _navMeshAgent;





	void Start () {
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

        if(_navMeshAgent == null)
        {
            Debug.LogError("Nav Not Working" + gameObject.name);
        }
        else
        {
            SetDestination();
        }
	}
	

	void Update () {
		
	}

    void SetDestination()
    {
        if(_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

}
