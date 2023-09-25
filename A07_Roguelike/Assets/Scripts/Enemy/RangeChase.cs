using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeChase : EnemyBehaviour
{
    private int _priority = 1;
    // ���������� ������ ���ݹ������� �߰��� �Ѵ�.

    // ������ �켱������ �̵����� �����ؼ� �����Ҽ��ִٸ� �̵����� �ʵ��� ������ָ�
    // ���� ���Ÿ������� ������ �̵��� ���� �ʿ�� ������?

    // ��� �Ÿ��� ������ ����� ����°� ������
    [SerializeField][Range(0f, 100f)] private float followRange;
    [SerializeField][Range(0f, 100f)] private float attackRange;
    [SerializeField][Range(0f, 100f)] private float speed;

    protected void Start()
    {
        Priority = _priority;
        CheckBehaviour += CheckRangeChase;
        OnBehaviour += OnRangeChase;
    }

    private void OnRangeChase()
    {
        controller.rb2D.velocity = speed * controller.Direction;
    }

    private bool CheckRangeChase()
    {

        // �Ÿ��� follow Range���� ª��
        // attack Range���� ���� true
        float distance = controller.Distance;

        if (distance < followRange && distance > attackRange) 
        {
            return true;
        }

        controller.rb2D.velocity = Vector2.zero;
        return false;
        
    }
}
