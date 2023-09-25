using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EnemyBehaviour
{

    // �������� == ����?

    // Player���� �Ÿ��� ���� �Ÿ� �����̸� ����

    // ������ ���� gameObject�� �����ϰ� ���� Object�� ����?

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
