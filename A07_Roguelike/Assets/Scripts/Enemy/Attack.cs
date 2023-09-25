using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EnemyBehaviour
{

    // 근접공격 == 폭발?

    // Player와의 거리가 일정 거리 이하이면 폭발

    // 폭발은 현재 gameObject를 제거하고 폭발 Object를 생성?

    private int _priority = 2;
    [SerializeField][Range(0f, 100f)] private float attackRange;

    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        Priority = _priority;
        CheckBehaviour += CheckAtack;
        OnBehaviour += OnAttack;
    }

    private void OnAttack()
    {
        Destroy(gameObject);
    }

    private bool CheckAtack()
    {
        if (controller.Distance < attackRange)
        {
            return true;
        }

        return false;
    }

}
