using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Move : EnemyBehaviour
{
    protected override void Start()
    {
        base.Start();
        controller.enemyBehaviours.Enqueue(this);
    }
    protected override void Update()
    {
        base.Update();
    }

    public override bool CheckBehaviour()
    {
        float distance = controller.Distance;

        if (distance < enemySO.followRange && enemySO.attackRange < distance)
        {
            return true;
        }

        return false;
    }

    public override void OnBehaviour()
    {
        // 조건을 만족할 때만 이동하고
        // 조건과 상관없이 항상 후순위로 밀어준다.
        if (CheckBehaviour())
        {
            Vector2 direction = Vector2.zero;
            float distance = controller.Distance;

            direction = controller.Direction * enemySO.speed;

            animationController.Move(direction);
            rb2D.velocity = direction;
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            spriteRenderer.flipX = Mathf.Abs(rotZ) > 90;
        }
        

        controller.enemyBehaviours.Dequeue();
        controller.enemyBehaviours.Enqueue(this);
    }


}
