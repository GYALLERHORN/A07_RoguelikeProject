using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeepDistance : EnemyBehaviour
{
    // Ÿ���� Range ���� �ȿ� ���´ٸ� 

    // targetDistnae ��ŭ �Ÿ��� ������.

    [SerializeField][Range(0f, 100f)] protected float targetDistance;

    [SerializeField][Range(0f, 100f)] float runAwaySpeed;

    private float tempSpeed;

    protected void Start()
    {
        type = EnemyBehaviourType.Move;
        Priority = (int)type;
        tempSpeed = controller.speed;
    }
    protected override void Update()
    {
        base.Update();
        if (remainTime < 0 && !IsReady && CheckBehaviour())
        {
            controller.enemyBehaviours.Enqueue(this);
            IsReady = true;
        }
    }
    public override void OnBehaviour()
    {
        controller.speed = runAwaySpeed;
        controller.CallMoveEvent(-controller.Direction);

        if (controller.Distance > targetDistance)
        {
            controller.speed = tempSpeed;
            controller.enemyBehaviours.Dequeue();
            IsReady = false;
        }
    }

    public override bool CheckBehaviour()
    {
        float distance = controller.Distance;

        if (distance < range)
        {
            return true;

        }
        return false;
    }
}
