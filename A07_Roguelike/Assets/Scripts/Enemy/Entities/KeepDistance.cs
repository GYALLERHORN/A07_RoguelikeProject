using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDistance : EnemyBehaviour
{
    // Ÿ���� Range ���� �ȿ� ���´ٸ� �Ÿ��� ������.
    protected void Start()
    {
        type = EnemyBehaviourType.Move;
        Priority = (int)type;
    }
    public override void OnBehaviour()
    {
        controller.rb2D.velocity = controller.speed * controller.Direction;
    }

    public override bool CheckBehaviour()
    {
        float distance = controller.Distance;

        if (distance < controller.followRange)
        {
            return true;

        }
        return false;
    }
}
