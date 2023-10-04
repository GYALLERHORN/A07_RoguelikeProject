using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;

    public void SetItem(Item _item)
    {
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.itemName = _item.itemName;
        item.attackSO = _item.attackSO;
        item.statsChangeType = _item.statsChangeType;
        item.maxHealth = _item.maxHealth;
        item.speed = _item.speed;
        item.canBePickedUpBy = _item.canBePickedUpBy;

        // 이미지 표현
        image = GetComponent<SpriteRenderer>();
        image.sprite = _item.itemImage;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (item.canBePickedUpBy.value == (item.canBePickedUpBy.value | (1 << other.gameObject.layer)))
        {

            if (item.itemType == ItemType.Consumables) // 소모품이면 statshandler에 바로 적용
            {
                CharacterStatsHandler statsHandler = other.gameObject.GetComponent<CharacterStatsHandler>();
                statsHandler.AddStatModifier(item);
            }

            else if (item.itemType == ItemType.Portion)
            {
                HealthController healthController = other.gameObject.GetComponent<HealthController>();
                healthController.ChangeHealth(item.maxHealth);
            }

            else if (item.itemType == ItemType.Equipment) // 장비이면 인벤토리에 추가
            {
                other.gameObject.GetComponent<InventoryHandler>().AddItem(item);
            }

            DestroyItem(); // 아이템 소멸
        }
    }
}
