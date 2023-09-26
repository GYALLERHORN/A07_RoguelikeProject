using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : EnemyAnimation
{
    private static readonly int isRun = Animator.StringToHash("isRun");
    private static readonly int isHurt = Animator.StringToHash("isHurt");

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 obj)
    {
        animator.SetBool(isRun, obj.magnitude > .5f);
    }

    private void Attacking(AttackSO obj)
    {

    }
   

    private void Hurt()
    {
        animator.SetBool(isHurt, true);
    }

    private void InvincibilityEnd()
    {
        animator.SetBool(isHurt, false);
    }



}
