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
        CoolTime += OnCoolTime;
    }

    // Update���� ���Ǵ� �޼���
    private void OnRest()
    {
        if ((controller.state == EnemyState.Move) && controller.Distance < range)
        {
            controller.state = enemyState;
            state = State.Ready;
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

    // FixedUpdate���� ���Ǵ� �޼���
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
    private void Init()
    {
        controller.StopEnemy();
        rushState = RushState.Charge;
        remainchargingTime = chargingTime;
        startPos = transform.position;
        animationController.Move(Vector2.zero);
        
    }
    private void OnCharging()
    {
        remainchargingTime -= Time.deltaTime;
        if (remainchargingTime < waitTime)
        {
            rushState = RushState.Complete;
            return;
        }

        // Ÿ���� Ȯ���� Ÿ�̹��� ������ �˱����ؼ�
        direction = controller.Direction;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        controller.SpriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        rushRange.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        rushRange.SetActive(true);

    }
    private void ReadyRush()
    {
        remainchargingTime -= Time.deltaTime;
        if(remainchargingTime < 0)
        {
            rushRange.SetActive(false);
            rushState = RushState.Rush;
        }
    }
    private void OnRush()
    {
        controller.Rb2D.velocity = direction * speed;
        animationController.Move(direction);
        controller.Collider.isTrigger = true;
        // �̵��� �Ÿ��� ������������ ��ǥ���������� �Ÿ��� ��
        if (Vector2.Distance(startPos, (Vector2)transform.position) > rushDistance)
        {
            controller.Rb2D.velocity = Vector2.zero;
            remainTime = coolTime;
            controller.Collider.isTrigger = false;
            state = State.CoolTime;
            controller.state = EnemyState.Move;
            rushState = RushState.Rest;
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
