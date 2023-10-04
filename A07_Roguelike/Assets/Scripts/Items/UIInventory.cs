using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UIInventory : UIBase
{
    public List<Item> itemList;
    public GameObject player;
    public Item startItem;
    public int itemCount = 0;
    private GameObject currentSlot;

    public void Initialize(GameObject player)
    {
        this.player = player;
    }

    // Start is called before the first frame update
    public void AddItem(Item _item)
    {
        itemList.Add(_item);
        // TO DO (�κ��丮 UI)
        currentSlot = transform.GetChild(itemCount + 2).gameObject;
        currentSlot.GetComponent<Slot>().item = _item;
        currentSlot.GetComponent<Slot>().player = player;
        currentSlot.GetComponent<Slot>().UpdateSlotUI();
        itemCount++;
    }

    public void Start()
    {
        AddItem(startItem); // ���� ������ ����ֱ�
        transform.GetChild(2).gameObject.GetComponent<Slot>().EquipItem(); // �ʱ� ������ ����
    }
}
