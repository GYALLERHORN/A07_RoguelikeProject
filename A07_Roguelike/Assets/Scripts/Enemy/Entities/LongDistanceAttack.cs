using UnityEngine;

public class LongDistanceAttack : EnemyBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        enemyState = EnemyState.Attack;
        state = State.CoolTime;
        
    }
    protected override void Start()
    {
        base.Start();
        _projectileManager = ProjectileManager.instance;
        Rest += OnRest;
        CoolTime += OnCoolTime;
    }
    private void OnRest()
    {
        if(controller.state == EnemyState.Move || controller.state == EnemyState.Move && CheckCondition())
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

    public override void OnBehaviour()
    {
        remainTime = coolTime;
        state = State.CoolTime;
        CreateProjectile(statsSO);
        controller.state = EnemyState.Move;
        controller.ReInsert(enemyState);         
    }     
    private void CreateProjectile(RangedAttackData rangedAttackData)
    {
        
        _projectileManager.ShootBullet(
                projectileSpawnPosition.position,
                controller.Direction,
                rangedAttackData
                );
    }

    private bool CheckCondition()
    {
        return controller.Distance < range;
    }

    [SerializeField] private RangedAttackData statsSO;
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField][Range(1f, 100f)] private float remainTime;
    [SerializeField][Range(1f, 100f)] private float coolTime;
    [SerializeField][Range(1f, 100f)] private float range;
    private ProjectileManager _projectileManager;


}
