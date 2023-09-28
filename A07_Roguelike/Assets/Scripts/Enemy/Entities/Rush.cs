using System.Resources;
using UnityEditor.VersionControl;
using UnityEngine;
public class Rush : EnemyBehaviour
{
    enum RushState
    {
        Rest,
        Charge,
        Complete,
        Rush,
    }
    protected override void Awake()
    {
        base.Awake();
        remainTime = Random.Range(1f, coolTime);
        state = State.CoolTime;
        enemyState = EnemyState.Skill;

    }
    protected override void Start()
    {
        base.Start();
        Rest += OnRest;
        Using += OnUsing;
        CoolTime += OnCoolTime;
    }
    public override void OnBehaviour()
    {
        state = State.Using;

        switch (rushState)
        {
            case RushState.Rest:
                Init();
                break;
            case RushState.Charge:
                OnCharging();
                break;
            case RushState.Complete:
                ReadyRush();
                break;
            case RushState.Rush:
                OnRush();
                break;

        }

    }
    private void OnRest()
    {
        if ((controller.state == EnemyState.Move) && controller.Distance < range)
        {
            controller.state = enemyState;
            state = State.Ready;
        }

    }
    private void OnUsing()
    {
        if (rushState == RushState.Charge)
        {
            remainchargingTime -= Time.deltaTime;
            if (remainchargingTime < 0)
            {
                rushState = RushState.Complete;
            }
        }
    }
    private void OnCoolTime()
    {
        remainTime -= Time.deltaTime;
        if (remainTime < 0f)
        {
            state = State.Rest;
        }
    }
    private void Init()
    {
        rushState = RushState.Charge;
        remainchargingTime = chargingTime;
        startPos = transform.position;
        controller.StopEnemy();
        animationController.Move(Vector2.zero);
        
    }
    private void OnCharging()
    {
        if(remainchargingTime < waitTime)
        {
            return;
        }

        // 타겟이 확정된 타이밍의 방향을 알기위해서
        direction = controller.Direction;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        controller.SpriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        rushRange.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        rushRange.SetActive(true);

    }
    private void ReadyRush()
    {
        rushRange.SetActive(false);
        rushState = RushState.Rush;
    }
    private void OnRush()
    {
        controller.Rb2D.velocity = direction * speed;
        animationController.Move(direction);
        controller.Collider.isTrigger = true;
        // 이동한 거리와 시작지점에서 목표지점까지의 거리를 비교
        if (Vector2.Distance(startPos, (Vector2)transform.position) > rushDistance)
        {
            controller.Rb2D.velocity = Vector2.zero;
            remainTime = coolTime;
            state = State.CoolTime;
            rushState = RushState.Rest;
            controller.Collider.isTrigger = false;
            controller.state = EnemyState.Move;
            controller.ReInsert(enemyState);
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
    Vector2 direction = Vector2.zero;
    Vector2 startPos = Vector2.zero;
    private float remainTime;
    RushState rushState = RushState.Rest;
}
