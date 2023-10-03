using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public GameObject player;
    private bool isEquipped = false;

    public void UpdateSlotUI()
    {
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.itemImage; // Item Image¿« sprite ∫Ø∞Ê
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


    public void EquipItem()
    {
        if(!isEquipped)
        {
            CharacterStatsHandler statsHandler = player.GetComponent<CharacterStatsHandler>();
            statsHandler.AddStatModifier(item);
            transform.GetChild(1).gameObject.SetActive(true);
            isEquipped = true;
        }
    }

    public void UnEquipItem()
    {
        if (isEquipped)
        {
            CharacterStatsHandler statsHandler = player.GetComponent<CharacterStatsHandler>();
            statsHandler.RemoveStatModifier(item);
            transform.GetChild(1).gameObject.SetActive(false);
            isEquipped = false;
        }
    }
}
