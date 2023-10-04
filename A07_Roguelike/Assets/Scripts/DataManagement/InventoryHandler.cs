using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryHandler : MonoBehaviour
{
    public List<Item> itemList;
    public Item startItem;
    public GameObject player;
    public int itemCount = 0;
    public int currentItemIdx = 0;


    public void Initialize(InventoryHandler inventoryHandler)
    {
        itemList = inventoryHandler.itemList;
        startItem = inventoryHandler.startItem;
        itemCount = inventoryHandler.itemCount;
        currentItemIdx = inventoryHandler.currentItemIdx;
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
        statsHandler.AddStatModifier(startItem);
    }

    // test
    private void Start()
    {
        InitItem(player.GetComponent<CharacterStatsHandler>());
    }

}
