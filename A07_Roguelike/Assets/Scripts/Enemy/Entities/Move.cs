using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Move : EnemyBehaviour
{
    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float speed;

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
        
        if (attackRange < distance && distance < followRange)
        {
            return true;
        }

        return false;
    }

    public override void OnBehaviour()
    {
        if (CheckBehaviour())
        {
            Vector2 direction = controller.Direction * speed;
            animationController.Move(direction);
            rb2D.velocity = direction;
        }
        
        controller.enemyBehaviours.Dequeue();
        controller.enemyBehaviours.Enqueue(this);
    }


}
