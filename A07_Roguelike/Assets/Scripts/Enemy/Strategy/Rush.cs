using System.Resources;
using UnityEditor.VersionControl;
using UnityEngine;
public class Rush : EnemyBehaviour, IBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb2D;
    private Collider2D _collider2D;
    enum RushStep
    {
        Rest,
        Charge,
        Complete,
        Rush,
    }
    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        remainTime = Random.Range(1f, coolTime);
    }

    private StrategyState _state = StrategyState.CoolTime;
    private StratgeyType _type = StratgeyType.Skill;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type;}

    public void OnRest()
    {
        if (!(Distance < range)) return;
        
        switch (CurrentBehaviourType())
        {
            case null:
            case StratgeyType.Move:
            case StratgeyType.Attack:
                StartAction(this);
                break;
            default:
                break;

        }
    }
    public void OnAction()
    {
        switch (rushState)
        {
            case RushStep.Rest:
                Init();
                break;
            case RushStep.Charge:
                OnCharging();
                break;
            case RushStep.Complete:
                ReadyRush();
                break;
            case RushStep.Rush:
                OnRush();
                break;
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

    #region OnAction()
    private void Init()
    {
        _rb2D.velocity = Vector2.zero;
        animationController.Move(Vector2.zero);

        rushState = RushStep.Charge;
        remainchargingTime = chargingTime;
        startPos = transform.position;
    }
    private void OnCharging()
    {
        remainchargingTime -= Time.deltaTime;
        if (remainchargingTime < waitTime)
        {
            rushState = RushStep.Complete;
            return;
        }

        // 타겟이 확정된 타이밍의 방향을 알기위해서
        direction = Direction;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _spriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        rushRange.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        rushRange.SetActive(true);
    }
    private void ReadyRush()
    {
        remainchargingTime -= Time.deltaTime;
        if (remainchargingTime < 0)
        {
            rushRange.SetActive(false);
            rushState = RushStep.Rush;
        }
    }
    private void OnRush()
    {
        _rb2D.velocity = direction * speed;
        animationController.Move(direction);
        // 이동한 거리와 시작지점에서 목표지점까지의 거리를 비교
        if (Vector2.Distance(startPos, (Vector2)transform.position) > rushDistance)
        {
            _rb2D.velocity = Vector2.zero;
            remainTime = coolTime;
            rushState = RushStep.Rest;
            EndAction(this);
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (State == StrategyState.Action && rushState == RushStep.Rush)
        {
            GameObject go = collision.gameObject;
            if (go == null) return;

            if (go.CompareTag("Player"))
            {
                HealthController hc = go.GetComponent<HealthController>();

                if (hc == null) return;
                hc.ChangeHealth(-damage);

            }
        }
    }

    [SerializeField] private GameObject rushRange;
    [SerializeField][Range(0f, 100f)] float range;
    [SerializeField][Range(0f, 100f)] float coolTime;
    [SerializeField][Range(0f, 100f)] float chargingTime;
    [SerializeField][Range(0f, 100f)] float waitTime;
    [SerializeField][Range(0f, 100f)] float rushDistance;
    [SerializeField][Range(0f, 100f)] float remainchargingTime;
    [SerializeField][Range(0f, 100f)] float speed;
    [SerializeField][Range(0, 20)] int damage;
    Vector2 direction = Vector2.zero;
    Vector2 startPos = Vector2.zero;
    private float remainTime;
    RushStep rushState = RushStep.Rest;

    
}
