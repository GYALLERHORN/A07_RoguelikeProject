using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<Item> itemList;
    public GameObject player;
    // Start is called before the first frame update
    public void AddItem(Item _item)
    {
        itemList.Add(_item);
        // TO DO (인벤토리 UI)
    }

    public void EquipItem(Item _item)
    {
        CharacterStatsHandler statsHandler = player.GetComponent<CharacterStatsHandler>();
        statsHandler.AddStatModifier(_item);
    }

    public void UnEquipItem(Item _item)
    {
        CharacterStatsHandler statsHandler = player.GetComponent<CharacterStatsHandler>();
        statsHandler.RemoveStatModifier(_item);
    }

}
