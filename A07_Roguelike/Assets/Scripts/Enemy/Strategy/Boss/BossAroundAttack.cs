using UnityEngine; 

public class BossAroundAttack : EnemyBehaviour, IBehaviour
{
    private StrategyState _state = StrategyState.CoolTime;
    private StratgeyType _type = StratgeyType.Skill;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }

    private Rigidbody2D _rb2D;

    [SerializeField] private GameObject _attackRange;
    private Range _attackRangeScript;
    private SpriteRenderer _attackRangeSpriteRenderer;
    [SerializeField] private Color _attackRangeColor;
    [SerializeField][Range(1f, 100f)] private float remainTime;
    [SerializeField][Range(1f, 100f)] private float coolTime;
    [SerializeField][Range(1f, 100f)] private float range;
    [SerializeField][Range(1f, 100f)] private float maxRange;
    [SerializeField][Range(1f, 100f)] private float chargeTime;
    [SerializeField][Range(1f, 100f)] private float delay;
    [SerializeField][Range(1f, 100f)] private float damageCoefficeint;

    protected override void Start()
    {
        base.Start();
        _rb2D = GetComponent<Rigidbody2D>();
        _attackRangeScript = _attackRange.GetComponent<Range>();
        _attackRangeSpriteRenderer = _attackRange.GetComponent <SpriteRenderer>();
    }

    // range의 크기가 1~20까지 늘어나다가 폭발!
    public void OffAction()
    {
        _attackRange.SetActive(false);
        _attackRange.transform.localScale = new Vector3(1, 1, 0);
        _attackRangeScript.OffRange();
        remainTime = coolTime;
    }

    public void OnAction()
    {
        if (_attackRange.transform.localScale.x < maxRange)
        {
            _attackRangeScript.OnRange();
            _attackRange.SetActive(true);
            _attackRangeSpriteRenderer.color = _attackRangeColor;
            _rb2D.velocity = Vector2.zero;
            _attackRange.transform.localScale += new Vector3(1, 1, 0) * (maxRange - 1) / chargeTime * Time.deltaTime;
            remainTime = delay;
        }
        else
        {
            remainTime -= Time.deltaTime;

            if(remainTime < 0)
            {
                if (_attackRangeScript.collidePlayer != null)
                {
                    animationController.Attack();
                    HealthController hc = _attackRangeScript.collidePlayer.GetComponent<HealthController>();

                    hc.ChangeHealth(-(int)(stats.attackSO.power * damageCoefficeint));

                }

                EndAction(this);
            }
        }
        
    }

    public void OnCoolTime()
    {
        remainTime -= Time.deltaTime;
        if (remainTime < 0f)
        {
            remainTime = delay;
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