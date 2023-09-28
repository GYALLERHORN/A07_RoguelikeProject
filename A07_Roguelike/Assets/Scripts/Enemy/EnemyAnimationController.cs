using UnityEngine;

public class EnemyAnimationController : EnemyAnimation
{
    private static readonly int isRun = Animator.StringToHash("isRun");
    private static readonly int isHurt = Animator.StringToHash("isHurt");
    private static readonly int isDeath = Animator.StringToHash("isDeath");

    private HealthController _healthSystem;

    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<HealthController>();
    }



    protected virtual void Start()
    {
        if (_healthSystem != null) 
        {
            _healthSystem.OnDamage += Hurt;
            _healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        }

    }
    public void Move(Vector2 obj)
    {
        animator.SetBool(isRun, obj.magnitude > .5f);
    }

    public void Death()
    {
        animator.SetBool(isDeath, true);
    }

    public void Attacking(AttackSO obj)
    {

    }


    public void Hurt()
    {
        animator.SetBool(isHurt, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(isHurt, false);
    }



}
