using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
public class Player : Entity {

    [SerializeField, HideInInspector] private int level = 1;
    [SerializeField, HideInInspector] private int exp;

    [SerializeField, HideInInspector] private int totalKills;

    PlayerMove playerMove;
    PlayerWeapon playerWeapon;

    private bool inBase;

    void Start () 
    {
        playerMove = GetComponent<PlayerMove>();
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
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
    }
}
