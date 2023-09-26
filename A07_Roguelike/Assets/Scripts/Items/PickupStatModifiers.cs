using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupStatModifiers : PickupItem // Stat ���� ����
{
    [SerializeField] private List<CharacterStats> statsModifier;
    [SerializeField] private bool isEquipment; // true : ��� , false : �Ҹ�ǰ
    [SerializeField] private GameObject Player; // �׽�Ʈ��
    private bool isEquipped = false;
    protected override void OnPickedUp(GameObject receiver)
    {
        if (!isEquipment) // �Ҹ�ǰ�̶�� �ٷ� ����
        {
            UseItem();
        }

        else
        {
            // �� object�� �κ��丮�� �߰�
        }
    }


    //private void UseItem(GameObject receiver)
    //{
    //    CharacterStatsHandler statsHandler = receiver.GetComponent<CharacterStatsHandler>();
    //    foreach (CharacterStats stat in statsModifier)
    //    {
    //        statsHandler.AddStatModifier(stat);
    //    }
    //}

    // test
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