using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Range : MonoBehaviour
{
    // 범위를 지정해서 데미지를 주는 범위 오브젝트에서 사용할 클래스

    private bool isUse = false;
    private int _damage;

    public void Use(int damage)
    {
        _damage = damage;
        isUse = true;
    }

    public void OffRange()
    {
        _damage = 0;
        isUse = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUse)
        {
            GameObject go = collision.gameObject;
            if (go == null) return;

            if (go.CompareTag("Player"))
            {
                HealthController hc = go.GetComponent<HealthController>();
                if (hc == null) return;

                hc.ChangeHealth(-(int)_damage);
            }
        }
    }
    
}
