using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift.Client.Unity;
using DarkRift;

[RequireComponent(typeof(PlayerMove))]
public class Player : Entity {

    [SerializeField, HideInInspector] private int level = 1;
    [SerializeField, HideInInspector] private int exp;

    [SerializeField, HideInInspector] private int totalKills;

    PlayerMove playerMove;
    PlayerWeapon playerWeapon;

    private bool inBase;

    const byte MOVEMENT_TAG = 1;

    [SerializeField]
    [Tooltip("The distance we can move before we send a position update.")]
    float moveDistance = 0.05f;

    public UnityClient Client { get; set; }

    public Vector3 lastPosition;

    void Awake()
    {
        lastPosition = transform.position;
    }

    void Start () 
    {
        playerMove = GetComponent<PlayerMove>();
        playerWeapon = GetComponentInChildren<PlayerWeapon>();

        health = maxHealth;
    }

    public PlayerWeapon GetPlayerWeapon () 
    {
        return playerWeapon;
    }

    public int GetLevel () 
    {
        return level;
    }

    public int GetExp () 
    {
        return exp;
    }

    public void SetBase (bool _inBase) 
    {
        inBase = _inBase;
    }

    [SerializeField] private Weapon toCall;
    void WeaponRequest () 
    {
        //call ui manager

        //get input from the manager as to which gun to use

        Debug.Log("Calling new weapon...");
        //Get the gamemanager to drop a new weapon box in the base
    }

    public void Save () 
    {
        SaveLoadManager.SavePlayer(this);
        Debug.Log("Player Data Saved");
    }

    public void Load () 
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && inBase)
        {
            WeaponRequest();
        }

        if (Vector3.Distance(lastPosition, transform.position) > moveDistance)
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(transform.position.x);
				writer.Write(transform.position.y);
                writer.Write(transform.position.z);

				using (Message message = Message.Create(Tags.MovePlayerTag, writer))
					Client.SendMessage(message, SendMode.Unreliable);
			}
            
            lastPosition = transform.position;
        }
    }
}
