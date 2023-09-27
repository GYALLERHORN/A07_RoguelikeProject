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
    protected void Start()
    {
        controller.enemyBehaviours.Enqueue(this);
        remainTime = Random.Range(1f, coolTime);
        state = State.CoolTime;

        Ready += OnReady;
        Rest += OnRest;
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
    private void OnReady()
    {
        state = controller.Distance < range ? State.Ready : State.Rest;
    }
    private void OnRest()
    {
        OnReady();
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
        rushRange.SetActive(true);
    }
    private void OnCharging()
    {
        // 타겟이 확정된 타이밍의 방향을 알기위해서
        targetPos = (Vector2)controller.Target.transform.position * 1.3f;
        direction = (targetPos - (Vector2)transform.position).normalized;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        controller.SpriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        rushRange.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (remainchargingTime < 0)
        {
            rushState = RushState.Complete;
        }
    }
    private void ReadyRush()
    {
        targetMoveDistance = Vector2.Distance(startPos, targetPos);
        rushRange.SetActive(false);
        rushState = RushState.Rush;
    }
    private void OnRush()
    {
        controller.Rb2D.velocity = direction * speed;
        animationController.Move(direction);

        // 이동한 거리와 시작지점에서 목표지점까지의 거리를 비교
        if (Vector2.Distance(startPos, (Vector2)transform.position) > targetMoveDistance)
        {
            controller.Rb2D.velocity = Vector2.zero;
            remainTime = coolTime;
            targetPos = Vector2.zero;
            state = State.CoolTime;
            rushState = RushState.Rest;
            controller.ReInsert();
        }

    }

    [SerializeField] private GameObject rushRange;
    [SerializeField][Range(0f, 100f)] float range;
    [SerializeField][Range(0f, 100f)] float coolTime;
    [SerializeField][Range(0f, 100f)] float chargingTime;
    [SerializeField][Range(0f, 100f)] float remainchargingTime;
    [SerializeField][Range(0f, 100f)] float speed;
    Vector2 targetPos = Vector2.zero;
    Vector2 direction = Vector2.zero;
    Vector2 startPos = Vector2.zero;
    private float remainTime;
    private float targetMoveDistance;
    RushState rushState = RushState.Rest;
}
