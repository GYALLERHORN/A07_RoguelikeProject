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
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.itemImage; // Item Image의 sprite 변경
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
        transform.GetChild(1).gameObject.SetActive(true); // equip되었다는 표시
    }


    public void EquipItem()
    {
        if(!isEquipped)
        {
            int itemCount = transform.parent.gameObject.GetComponent<UIInventory>().itemList.Count;
            for(int i = 2; i<itemCount + 2; i++)
            {
                transform.parent.gameObject.transform.GetChild(i).GetComponent<Slot>().UnEquipItem(); // 현재 착용한 모든 아이템 해제
            }
            CharacterStatsHandler statsHandler = player.GetComponent<CharacterStatsHandler>();
            statsHandler.AddStatModifier(item);
            transform.GetChild(1).gameObject.SetActive(true); // equip되었다는 표시
            player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = item.itemImage; // 무기 sprite 변경
            player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); // 무기 소멸
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
            transform.GetChild(1).gameObject.SetActive(false); // equip 표시 해제
            player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); // 무기 소멸
            isEquipped = false;
            player.GetComponent<InventoryHandler>().currentItemIdx = 0;
        }
    }
}
