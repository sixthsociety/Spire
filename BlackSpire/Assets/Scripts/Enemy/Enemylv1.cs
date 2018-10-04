using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemylv1 : MonoBehaviour {
    //Variables
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBetweenShots;
    public float startBetweenShots;




    private Transform player;
    public GameObject projectile;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startBetweenShots;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Vector3.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        } else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;

        } else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if(timeBetweenShots <= 0)
        {

            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShots = startBetweenShots;

        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

	}
}
