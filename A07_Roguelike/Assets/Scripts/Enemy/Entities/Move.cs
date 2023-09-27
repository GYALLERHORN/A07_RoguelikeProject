using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Move : EnemyBehaviour
{
    enum MoveState
    {
        Default,
        KnockBack,
    }
    protected void Start()
    {
        controller.enemyBehaviours.Enqueue(this);
        state = State.Rest;
        Ready += OnReady;
        Rest += OnRest;
    }
    protected void FixedUpdate()
    {
        if (moveState == MoveState.KnockBack)
        {
            knockbackDuration -= Time.fixedDeltaTime;

            if (knockbackDuration <= 0.0f)
            {
                moveState = MoveState.Default;
            }
        }
    }
    private void OnReady()
    {
        float distance = controller.Distance;
        state =attackRange < distance && distance < followRange  ? State.Ready : State.Rest;
    }
    private void OnRest()
    {
        OnReady();
    }   
    public override void OnBehaviour()
    {
        Vector2 direction = controller.Direction;

        switch (moveState)
        {
            case MoveState.Default:
                animationController.InvincibilityEnd();
                animationController.Move(direction);
                break;
            case MoveState.KnockBack:
                direction += _knockback;
                animationController.Hurt();
                break;
        }
        controller.Rb2D.velocity = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * direction * speed;
        controller.ReInsert();
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        moveState = MoveState.KnockBack;
        _knockback = -(other.position - transform.position).normalized * power;
    }

    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float speed;
    private Vector2 _knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;
    MoveState moveState = MoveState.Default;
}


