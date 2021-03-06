﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Merchant : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Player player;
    [SerializeField] public MerchantInventory inventoryMerchant;
    [SerializeField] public GameObject prefabUI;
    public PlayerInventory InventoryPlayer { get => player.inventory; }
    public MerchantInventory InventoryMerchant { get => inventoryMerchant; }

    public MerchantUI merchantUI;
    public PauseManager pauseManager;
    public GameObject inputManager;

    public static int goldModifier = 250;
    public static int woodModifier = 10;
    public static int scrapsModifier = 2;

    private static bool loadedScene;

    private void Awake()
    {  
        if (inventoryMerchant != null)
            inventoryMerchant = ClonableSO.Clone(inventoryMerchant);
        else 
            prefabUI.SetActive(false);
    }

    public void EnterInMerchant() 
    {
        if (!loadedScene)
        {
            prefabUI.SetActive(true);
            loadedScene = true;           
            pauseManager.Pause();
            inputManager.SetActive(false);
        }
            
    }

    public void ExitMerchant()
    {
        if (loadedScene)
        {
            prefabUI.SetActive(false);
            loadedScene = false;
            
            pauseManager.Unpause();
            inputManager.SetActive(true);
        }
    }

    public void ClientBuyItem(Item item, float price)
    {
        InventoryStorage storage = inventoryMerchant.GetStoredItem(item);
        if (player.inventory.Money >= price && storage != null)
        {
            player.inventory.Add(item);
            inventoryMerchant.Remove(item);

            player.inventory.Money -= price;
        }
        merchantUI.BuildUI();
    }

    public void ClientSellItem(Item item, float price)
    {
        InventoryStorage storage = player.inventory.GetStoredItem(item);
        if(storage != null && storage.count > 0)
        {
            player.inventory.Remove(item);
            inventoryMerchant.Add(item);
            player.inventory.Money += price;
        }

        merchantUI.BuildUI();
    }

    public void UpgradeBoatPlayer()
    {

        int boatNextLevel = player.LevelBoat + 1;

        if (goldModifier * boatNextLevel <= player.inventory.Money
            && scrapsModifier * boatNextLevel <= player.inventory.CountItem("Scraps")
            && woodModifier * boatNextLevel <= player.inventory.CountItem("Wood Plank")
            && player.MaxBoatUpgrade <= boatNextLevel)
        {
            player.Upgrade();
            uint scraps = (uint)(scrapsModifier * boatNextLevel);
            uint wood = (uint)(woodModifier * boatNextLevel);
            player.inventory.Remove(player.inventory.GetStoredItemByName("Scraps"), scraps);
            player.inventory.Remove(player.inventory.GetStoredItemByName("Wood Plank"), wood);
            player.inventory.Money -= goldModifier * boatNextLevel;
            
            merchantUI.BuildUI();
        }
    }
}
