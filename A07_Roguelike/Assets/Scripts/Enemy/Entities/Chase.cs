using System;
using Unity.VisualScripting;
using UnityEngine;

public class Chase : EnemyBehaviour
{
    private static readonly int IsRun= Animator.StringToHash("isRun");

    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        type = EnemyBehaviourType.Move;
        Priority = (int)type;
    }

    public override void OnBehaviour()
    {
        Vector2 direction = controller.Direction;
        controller.rb2D.velocity = controller.speed * direction;

        if (animator != null)
        {
            animator.SetBool(IsRun, true);
        }
        
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
