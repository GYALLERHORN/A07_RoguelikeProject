using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Bomb : EnemyBehaviour
{
    private static readonly int isDeath = Animator.StringToHash("isDeath");
    protected override void Awake()
    {
        base.Awake();
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
        controller.isDead = true;
        animator.SetBool(isDeath, true);
        transform.localScale = Vector3.one * 1.5f;
        Destroy(gameObject, .35f);
         
    }

    public override bool CheckBehaviour()
    {
        if (remainTime > 0) return false;

        if (controller.Distance > range) return false;
   
        return true;
    }
}
