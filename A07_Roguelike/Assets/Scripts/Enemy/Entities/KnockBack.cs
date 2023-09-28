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
    protected override void Start()
    {
        base.Start();
        Using += OnUsing;
    }
    public override void OnBehaviour()
    {
        state = State.Using;
        Vector2 direction = controller.Direction + _knockback;
        animationController.Hurt();
        controller.Rb2D.velocity = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * direction * speed;     
    }

    private void OnUsing()
    {
        knockbackDuration -= Time.deltaTime;

        if (knockbackDuration <= 0.0f)
        {
            controller.StopEnemy();
            _knockback = Vector2.zero;
            animationController.InvincibilityEnd();
            state = State.Rest;
            controller.state = EnemyState.Move;
            controller.ReInsert(enemyState);
        }   
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        _knockback = -(other.position - transform.position).normalized * power;
        state = State.Ready;
        controller.state = EnemyState.Hurt;

    }

    private Vector2 _knockback = Vector2.zero;
    [SerializeField]private float knockbackDuration = 0.0f;
    [SerializeField] float speed;
}
