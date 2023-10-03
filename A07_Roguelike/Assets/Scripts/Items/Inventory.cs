using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<Item> itemList;
    public GameObject player;
    private GameObject currentSlot;
    private int itemCount = 0;
    // Start is called before the first frame update
    public void AddItem(Item _item)
    {
        itemList.Add(_item);
        // TO DO (인벤토리 UI)
        currentSlot = transform.GetChild(itemCount + 2).gameObject;
        currentSlot.GetComponent<Slot>().item = _item;
        currentSlot.GetComponent<Slot>().player = player;
        currentSlot.GetComponent<Slot>().UpdateSlotUI();
        itemCount++;
    }
}
