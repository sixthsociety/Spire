using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Tower : MonoBehaviour {

    [SerializeField] private bool doesHeal;
    [SerializeField] private float healRate = .3f;

    private List<Player> playersInBase = new List<Player>();

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Player justEntered = col.GetComponent<Player>();
            playersInBase.Add(justEntered);
            justEntered.SetBase(true);

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
            Player justExited = col.GetComponent<Player>();
            playersInBase.Remove(justExited);
            justExited.SetBase(false);

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
