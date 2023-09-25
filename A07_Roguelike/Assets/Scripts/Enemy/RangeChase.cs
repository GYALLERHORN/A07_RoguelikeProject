using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeChase : EnemyBehaviour
{
    private int _priority = 1;
    // 일정범위에 들어오면 공격범위까지 추격을 한다.

    // 공격의 우선순위를 이동보다 높게해서 공격할수있다면 이동하지 않도록 만들어주면
    // 굳이 원거리공격을 생각한 이동을 넣을 필요는 없을듯?

    // 대신 거리를 벌리는 기능을 만드는게 좋을듯
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

        // 거리가 follow Range보다 짧고
        // attack Range보단 길어야 true
        float distance = controller.Distance;

        if (distance < followRange && distance > attackRange) 
        {
            return true;
        }

        controller.rb2D.velocity = Vector2.zero;
        return false;
        
    }
}
