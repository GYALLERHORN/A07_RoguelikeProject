using UnityEngine; 

public class BossAroundAttack : EnemyBehaviour, IBehaviour
{
    private StrategyState _state = StrategyState.CoolTime;
    private StratgeyType _type = StratgeyType.Skill;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }

    private Rigidbody2D _rb2D;
    [SerializeField] private GameObject _attackRange;
    [SerializeField][Range(1f, 100f)] private float remainTime;
    [SerializeField][Range(1f, 100f)] private float coolTime;
    [SerializeField][Range(1f, 100f)] private float range;
    [SerializeField][Range(1f, 100f)] private float maxRange;
    [SerializeField][Range(1f, 100f)] private float chargeTime;

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    // range의 크기가 1~20까지 늘어나다가 폭발!
    public void OffAction()
    {
        _attackRange.transform.localScale = new Vector3(1, 1, 0);
        _attackRange.SetActive(false);
        remainTime = coolTime;
    }

    public void OnAction()
    {
        _attackRange.SetActive(true);
        _rb2D.velocity = Vector2.zero;
        _attackRange.transform.localScale += new Vector3(1,1,0) * (maxRange-1) / chargeTime * Time.deltaTime;

        if (_attackRange.transform.localScale.x > maxRange)
        {
            Debug.Log("Bomb");
            EndAction(this);
        }
    }

    public void OnCoolTime()
    {
        remainTime -= Time.deltaTime;
        if (remainTime < 0f)
        {
            State = StrategyState.Rest;
        }
    }

    public void OnRest()
    {
        if (Distance > range) return;

        switch (CurrentBehaviourType())
        {
            case StratgeyType.Skill:
                break;
            case StratgeyType.Dead:
                break;
            default:
                StartAction(this);
                break;
        }
    }
}