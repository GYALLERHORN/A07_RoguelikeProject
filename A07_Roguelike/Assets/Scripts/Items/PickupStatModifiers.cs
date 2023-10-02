using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupStatModifiers : PickupItem // Stat ���� ����
{
    [SerializeField] private List<CharacterStats> statsModifier;
    [SerializeField] private GameObject Player;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject Item;
    protected override void OnPickedUp(GameObject receiver)
    {
        if (!isEquipment) // �Ҹ�ǰ�̶�� �ٷ� ����
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if (!isEquipped)
        {
            CharacterStatsHandler statsHandler = Player.GetComponent<CharacterStatsHandler>();
            foreach (CharacterStats stat in statsModifier)
            {
                statsHandler.AddStatModifier(stat);
            }
            isEquipped = true;
        }

    }

    public void UnequipItem()
    {
        if (isEquipped)
        {
            CharacterStatsHandler statsHandler = Player.GetComponent<CharacterStatsHandler>();
            foreach (CharacterStats stat in statsModifier)
            {
                statsHandler.RemoveStatModifier(stat);
            }
            isEquipped = false;
        }
    }

}