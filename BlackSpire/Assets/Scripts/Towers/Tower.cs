using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Tower : MonoBehaviour {

    [SerializeField] private bool doesHeal;
    [SerializeField] private float healRate = .3f;

    private List<HealthBehaviour> playersInBase = new List<HealthBehaviour>();

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            HealthBehaviour justEntered = col.GetComponent<HealthBehaviour>();
            playersInBase.Add(justEntered);

            if (doesHeal)
            {
                InvokeRepeating("HealPlayers", 0, healRate);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            HealthBehaviour justExited = col.GetComponent<HealthBehaviour>();
            playersInBase.Remove(justExited);

            if (playersInBase.Count == 0) 
            {
                CancelInvoke("HealPlayers");
            }
        }
    }

    void HealPlayers () 
    {
        for (int i = 0; i < playersInBase.Count; i++)
        {
            playersInBase[i].Heal(1);
        }
    }
}
