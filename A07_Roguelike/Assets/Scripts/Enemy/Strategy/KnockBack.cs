using UnityEngine;

public class KnockBack : EnemyBehaviour, IBehaviour
{
    private Rigidbody2D _rb2D;
    private StrategyState _state = StrategyState.Rest;
    private StratgeyType _type = StratgeyType.Hurt;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }

    protected override void Awake()
    {
        base.Awake();
        _rb2D = GetComponent<Rigidbody2D>();
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        if (CurrentBehaviourType() == StratgeyType.Dead) return;
        knockbackDuration = duration;
        knockbackDirection = -(other.position - transform.position).normalized;
        speed = (knockbackDirection * power).magnitude / duration;
        animationController.Hurt();
        StartAction(this);
    }

    public void OnRest() {}
    public void OnAction()
    {
        _rb2D.velocity = knockbackDirection * speed;

        knockbackDuration -= Time.deltaTime;
        if (knockbackDuration <= 0.0f)
        {
            EndAction(this);
        }
    }
    public void OnCoolTime() { }

    private Vector2 knockbackDirection = Vector2.zero;
    private float knockbackDuration = 0.0f;
    private float speed = 0.0f;
    
    
}
