using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : EnemyBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        enemyState = EnemyState.Hurt;

    }
    public override void OnBehaviour()
    {
        state = State.Using;
        controller.Rb2D.velocity = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * knockbackDirection * speed;

        knockbackDuration -= Time.deltaTime;
        if (knockbackDuration <= 0.0f)
        {
            controller.StopEnemy();
            state = State.Rest;
            controller.state = EnemyState.Move;
            controller.ReInsert(enemyState);
        }
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        if (controller.state == EnemyState.Dead) return;
        knockbackDuration = duration;
        knockbackDirection = -(other.position - transform.position).normalized;
        speed = (knockbackDirection * power).magnitude / duration;
        state = State.Ready;
        controller.state = EnemyState.Hurt;
        animationController.Hurt();
    }
    private Vector2 knockbackDirection = Vector2.zero;
    private float knockbackDuration = 0.0f;
    private float speed = 0.0f;
    
    
}
