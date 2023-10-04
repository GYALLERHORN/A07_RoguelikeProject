using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public GameObject player;
    public bool isEquipped = false;
    public int slotIndex;

    public void UpdateSlotUI()
    {
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.itemImage; // Item Image�� sprite ����
        transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    public void EquipControl()
    {
        if(!isEquipped)
        {
            EquipItem();
        }

        else
        {
            UnEquipItem();
        }
    }

    public void MakeStatusEquipped()
    {
        isEquipped = true;
        transform.GetChild(1).gameObject.SetActive(true); // equip�Ǿ��ٴ� ǥ��
    }


    public void EquipItem()
    {
        if(!isEquipped)
        {
            int itemCount = transform.parent.gameObject.GetComponent<UIInventory>().itemList.Count;
            for(int i = 2; i<itemCount + 2; i++)
            {
                transform.parent.gameObject.transform.GetChild(i).GetComponent<Slot>().UnEquipItem(); // ���� ������ ��� ������ ����
            }
            CharacterStatsHandler statsHandler = player.GetComponent<CharacterStatsHandler>();
            statsHandler.AddStatModifier(item);
            transform.GetChild(1).gameObject.SetActive(true); // equip�Ǿ��ٴ� ǥ��
            player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = item.itemImage; // ���� sprite ����
            player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); // ���� �Ҹ�
            isEquipped = true;
            player.GetComponent<InventoryHandler>().currentItemIdx = slotIndex;
        }
    }

    public void UnEquipItem()
    {
        if (isEquipped)
        {
            CharacterStatsHandler statsHandler = player.GetComponent<CharacterStatsHandler>();
            statsHandler.RemoveStatModifier(item);
            transform.GetChild(1).gameObject.SetActive(false); // equip ǥ�� ����
            player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); // ���� �Ҹ�
            isEquipped = false;
            player.GetComponent<InventoryHandler>().currentItemIdx = 0;
        }
    }
}
