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
        // TO DO (�κ��丮 UI)
    }

    public void EquipItem(Item _item)
    {
        CharacterStatsHandler statsHandler = player.GetComponent<CharacterStatsHandler>();
        _item.EquipItem(statsHandler);
    }

    public void UnequipItem(Item _item)
    {
        CharacterStatsHandler statsHandler = player.GetComponent<CharacterStatsHandler>();
        _item.UnequipItem(statsHandler);
    }

}
