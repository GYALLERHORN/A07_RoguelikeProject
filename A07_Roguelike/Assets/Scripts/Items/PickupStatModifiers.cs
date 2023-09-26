using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupStatModifiers : PickupItem // Stat 변경 관리
{
    [SerializeField] private List<CharacterStats> statsModifier;
    [SerializeField] private bool isEquipment; // true : 장비 , false : 소모품
    [SerializeField] private GameObject Player; // 테스트용
    private bool isEquipped = false;
    protected override void OnPickedUp(GameObject receiver)
    {
        if (!isEquipment) // 소모품이라면 바로 적용
        {
            UseItem();
        }

        else
        {
            // 이 object를 인벤토리에 추가
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