using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum ItemType
{
    Equipment,
    Consumables,
    Portion,
}


[System.Serializable]
public class Item : CharacterStats
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public LayerMask canBePickedUpBy;

    private bool isEquipped = false;

    public void EquipItem(CharacterStatsHandler statsHandler)
    {
        if (!isEquipped)
        {
            statsHandler.AddStatModifier(this);
            isEquipped = true;
        }
    }

    public void UnequipItem(CharacterStatsHandler statsHandler)
    {
        if (isEquipped)
        {
            statsHandler.RemoveStatModifier(this);
            isEquipped = true;
        }
    }
}
