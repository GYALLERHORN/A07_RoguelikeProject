using UnityEngine;

public class BossAroundSafe : EnemyBehaviour, IBehaviour
{
    enum eSkillStep
    {
        Rest,
        Charge,
        Use,
    }
    private StrategyState _state = StrategyState.CoolTime;
    private StratgeyType _type = StratgeyType.Skill;
    private eSkillStep _step = eSkillStep.Rest;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }

    private Rigidbody2D _rb2D;
    [SerializeField] private GameObject _attackRange;
    [SerializeField] private GameObject _safeRange;

    [SerializeField] private Color _attackRangeColor;
    [SerializeField] private Color _safeRangeColor;

    private Range _attackRangeScript;
    private Range _safeRangeScript;
    
    private SpriteRenderer _safeRangeSpriteRenderer;

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
        _safeRangeScript = _safeRange.GetComponent<Range>();
        _safeRangeSpriteRenderer = _safeRange.GetComponent<SpriteRenderer>();
    }
   
    public void OffAction()
    {
        _attackRange.SetActive(false);
        _safeRange.SetActive(false);

        _safeRange.transform.localScale = new Vector3(1, 1, 0);
        _attackRangeScript.OffRange();
        _safeRangeScript.OffRange();
        remainTime = coolTime;
        _step = eSkillStep.Rest;
    }

    public void OnAction()
    {
        switch (_step)
        {
            case eSkillStep.Rest:
                OnInit();
                break;
            case eSkillStep.Charge:
                OnCharge();
                break;
            case eSkillStep.Use:
                OnUse();
                break;
            default:
                break;
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

    private void OnInit()
    {
        _rb2D.velocity = Vector2.zero;
        _attackRange.SetActive(true);
        _safeRange.SetActive(true);
        _attackRangeScript.OnRange();
        _safeRangeScript.OnRange();
        _safeRangeSpriteRenderer.color = _safeRangeColor;
        _step = eSkillStep.Charge;

    }

    private void OnCharge()
    {
        if (_safeRange.transform.localScale.x < maxRange)
        {
            _safeRange.transform.localScale += new Vector3(1, 1, 0) * (maxRange - 1) / chargeTime * Time.deltaTime;
        }
        else
        {
            _step = eSkillStep.Use;
            animationController.Attack();
            
            remainTime = delay;
        }
    }

    private void OnUse()
    {
        remainTime -= Time.deltaTime;

        if (remainTime > 0)
        {
            if (_attackRangeScript.collidePlayer != null && _safeRangeScript.collidePlayer == null)
            {
                HealthController hc = _attackRangeScript.collidePlayer.GetComponent<HealthController>();

                hc.ChangeHealth(-(int)(stats.attackSO.power * damageCoefficeint));

                EndAction(this);
            }
        }
        else
        {
            EndAction(this);
        }
    }

}