using UnityEngine;
using UnityEngine.InputSystem.XR;

public class KeepDistance : EnemyBehaviour, IBehaviour
{
    private Rigidbody2D _rb2D;
    private StrategyState _state = StrategyState.Rest;
    private StratgeyType _type = StratgeyType.Skill;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }

    protected override void Awake()
    {
        base.Awake();
        _rb2D = GetComponent<Rigidbody2D>(); 
    }
    public void OnRest()
    {
        if (!CheckCondition()) return;

        switch (CurrentBehaviourType())
        {
            case StratgeyType.Dead:
                break;
            default:
                StartAction(this);
                break;
        }
    }
    public void OnAction() 
    {
        Vector2 direction = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * -Direction * stats.speed * speedCoefficient;
        _rb2D.velocity = direction;
        animationController.Move(direction);

        if (Distance > targetDistance)
        {
            EndAction(this);
        }
    }

    public void OffAction()
    {
        _rb2D.velocity = Vector2.zero;
        animationController.Move(Vector2.zero);
        remainTime = coolTime;
    }
    public void OnCoolTime()
    {
        remainTime -= Time.deltaTime;
        if (remainTime < 0f)
        {
            State = StrategyState.Rest;
            remainTime = coolTime;
        }
    }

    private bool CheckCondition()
    {
        return Distance < range;
    }

    [SerializeField][Range(0f, 100f)] protected float targetDistance;
    [SerializeField][Range(0f, 100f)] float speedCoefficient;
    [SerializeField][Range(0f, 100f)] float remainTime;
    [SerializeField][Range(0f, 100f)] float coolTime;
    [SerializeField][Range(0f, 100f)] float range;
}
