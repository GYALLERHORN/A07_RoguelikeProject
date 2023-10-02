using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Equipment,
    Consumables,
}


[System.Serializable]
public class Item : CharacterStats
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public LayerMask canBePickedUpBy;
}
