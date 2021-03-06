﻿using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Weaponry weaponry = null;
    [SerializeField] public Post controlPost = null;
    [SerializeField] public RepairStation repairStation = null;
    [Header("Runtime")]
    [SerializeField] public PlayerInventory inventory = null;
    PlayerUpgrade upgrader = null;

    public int MaxBoatUpgrade { get => upgrader.upgrades.Length; }

    public void Shoot(float input)
    {
        if (input == 0 || !isActiveAndEnabled)
            return;
        weaponry.ShootAt(Utils.MousePositionOcean);
    }

    public void InitWeapons()
    {
        if (!weaponry) {
            weaponry = GetComponent<Weaponry>();
        }
        if (weaponry) {
            InputManager.Instance.AddAxisEvent("Fire1", Shoot);
        }
    }

    public void InitCrew()
    {
        if (controlPost) {
            // CrewMember playerCharacter = GetComponent<InventoryHolder>()
            controlPost.ForceHire(inventory.crewMembers[0]);
        }

    }

    private void Awake() {
        inventory = GetComponent<InventoryHolder>().inventory as PlayerInventory;
        upgrader = GetComponent<PlayerUpgrade>();
        InitWeapons();
        InitCrew();
    }


    private void Start() {
    }

    private void Update() {
        if (repairStation)
            repairStation.Use();
        weaponry.RotateToward(Utils.MousePositionOcean);
        foreach (CrewMember crewMember in inventory.crewMembers)
        {
            crewMember.Update();
        }

        // if (Input.GetKeyDown(KeyCode.U))
        //     Upgrade();
    }

    int _upgrade = 1;
    public int LevelBoat { get => _upgrade;}

    public void Upgrade()
    {
        _upgrade = upgrader.Upgrade() + 1;
    }


}
