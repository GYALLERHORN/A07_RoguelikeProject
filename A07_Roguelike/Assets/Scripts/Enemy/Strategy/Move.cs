using Unity.VisualScripting;
using UnityEngine;

public class Move : EnemyBehaviour, IBehaviour
{
    private Rigidbody2D _rb2D;

    [SerializeField] float followRange;
    [SerializeField] float attackRange;

    private StrategyState _state = StrategyState.Rest;
    private StratgeyType _type = StratgeyType.Move;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type;}

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
            case null:
            case StratgeyType.Move:
                StartAction(this);
                break;
            default:
                break;
        }

    }

    public void OnAction() 
    {
        Vector2 direction = Direction;
        _rb2D.velocity = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * direction * characterStatsHandler.CurrentStats.speed;
        animationController.Move(direction);

        if (!CheckCondition())
        {
            EndAction(this);
        }
    }
    public void OnCoolTime()
    {
        State = StrategyState.Rest;
    }

    public void OffAction()
    {
        _rb2D.velocity = Vector2.zero;
        animationController.Move(Vector2.zero);
    }

    private bool CheckCondition()
    {
        float distance = Distance;
        return attackRange < distance && distance < followRange ? true : false;
    }


}


