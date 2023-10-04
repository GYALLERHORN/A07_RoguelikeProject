using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public List<Item> itemList;
    public Item startItem;
    public GameObject player;
    public int itemCount = 0;
    public int currentItemIdx;


    public void Initialize(InventoryHandler inventoryHandler)
    {
        itemList = inventoryHandler.itemList;
        startItem = inventoryHandler.startItem;
        itemCount = inventoryHandler.itemCount;
        currentItemIdx = inventoryHandler.currentItemIdx;
        player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = itemList[currentItemIdx].itemImage;
    }

    public void AddItem(Item _item)
    {
        itemList.Add(_item);
        // TO DO (�κ��丮 UI)
        itemCount++;
    }

    public void InitItem(CharacterStatsHandler statsHandler)
    {
        AddItem(startItem); // ���� ������ ����ֱ�
        currentItemIdx = 0;
        statsHandler.AddStatModifier(startItem);
    }

}
