﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Merchant")]
public class MerchantInventory : Inventory
{
    public List<Item> tradableItems = new List<Item>();
    public float priceModifier;
}