﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MerchantManager : MonoBehaviour
{
    public Inventory inventoryPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterInMerchant(GameObject obj)
    {
        if(!UnityEngine.SceneManagement.SceneManager.GetSceneByName("Merchant").isLoaded)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Merchant", LoadSceneMode.Additive);
    }

    public void ExitMerchant()
    {
        if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName("Merchant").isLoaded)
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Merchant");
    }

    public void ClientBuyItem(Collectable item, float price)
    {
        if (inventoryPlayer.Money >= price)
        {
            inventoryPlayer.AddItemToInventory(item);
            inventoryPlayer.Money -= price;
        }
    }

    public void ClientSellItem(Collectable item, float price)
    {
        if(inventoryPlayer.m_content.ContainsKey(item) && inventoryPlayer.m_content[item] > 0)
        {
            inventoryPlayer.RemoveItemFromInventory(item);
            inventoryPlayer.Money += price;
        }
    }
}
