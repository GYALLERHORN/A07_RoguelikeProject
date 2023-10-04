using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UIInventory : UIBase
{
    public List<Item> itemList;
    public GameObject player;
    public int currentItemIdx;
    private Slot currentSlot;

    public void Initialize(GameObject player)
    {
        this.player = player;
        InventoryHandler inventoryHandler = player.GetComponent<InventoryHandler>();
        itemList = inventoryHandler.itemList;
        currentItemIdx = inventoryHandler.currentItemIdx;

        for (int i = 0; i < itemList.Count; i++)
        {
            AddItem(itemList[i], i);
            if(i == currentItemIdx)
            {
                transform.GetChild(i + 2).gameObject.GetComponent<Slot>().MakeStatusEquipped();
            }
        }
    }

    // Start is called before the first frame update
    public void AddItem(Item _item, int slotIndex)
    {
        currentSlot = transform.GetChild(slotIndex + 2).gameObject.GetComponent<Slot>();
        currentSlot.item = _item;
        currentSlot.player = player;
        currentSlot.UpdateSlotUI();
    }

}
