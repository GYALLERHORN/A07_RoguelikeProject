using UnityEngine;

public class RangeAttack : EnemyBehaviour
{
    protected void Start()
    {
        _projectileManager = ProjectileManager.instance;
        controller.enemyBehaviours.Enqueue(this);
        Ready += OnReady;
        Rest += OnRest;
        CoolTime += OnCoolTime;
        state = State.CoolTime;
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
    public override void OnBehaviour()
    {
        controller.Rb2D.velocity = Vector3.zero;
        remainTime = coolTime;
        state = State.CoolTime;
        CreateProjectile((EnemySO)controller.StatsHandler.CurrentStates.attackSO);
        controller.ReInsert();         
    }     
    private void CreateProjectile(RangedAttackData rangedAttackData)
    {
        
        _projectileManager.ShootBullet(
                projectileSpawnPosition.position,
                controller.Direction,
                rangedAttackData
                );
    }

    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField][Range(1f, 100f)] private float remainTime;
    [SerializeField][Range(1f, 100f)] private float coolTime;
    [SerializeField][Range(1f, 100f)] private float range;
    private ProjectileManager _projectileManager;


}
