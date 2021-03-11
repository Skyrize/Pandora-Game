﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemEvent : UnityEvent<Item>
{
}

[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ClonableSO
{
    [Header("Settings")]
    [Min(0f)] public float weight = 1.0f;

    [Header("References")]
    public GameObject prefab;
    public Sprite icon;

    public string Name = "default";

    public bool Equals(Item other)
    {
        return other != null && other.Name == this.Name;
    }

    public override int GetHashCode()
    {
        if (Name == null) return 0;
        return Name.GetHashCode();
    }
    
    protected void OnValidate() {
        if (prefab) {
            ItemObject holder = prefab.GetComponent<ItemObject>();

            if (!holder) {
                holder = prefab.AddComponent<ItemObject>();
            }
            if (!holder.item)
                holder.item = this;

        }
        Name = name;
    }

    override protected ClonableSO Clone()
    {
        var clone = base.Clone() as Item;

        clone.Name = this.Name;
        return clone;
    }
    

}
