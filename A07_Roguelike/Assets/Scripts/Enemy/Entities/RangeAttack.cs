using UnityEngine;

public class RangeAttack : EnemyBehaviour
{
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] [Range(1f, 100f)] private float remainTime;
    [SerializeField][Range(1f, 100f)] private float coolTime;
    [SerializeField][Range(1f, 100f)] private float range;
    
    private ProjectileManager _projectileManager;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        _projectileManager = ProjectileManager.instance;
        controller.enemyBehaviours.Enqueue(this);
    }

    protected override void Update()
    {
        base.Update();
        remainTime-= Time.deltaTime;
    }
    public override bool CheckBehaviour()
    {
        // ���� ��Ÿ� �ȿ� ���Դٸ� �����ε�

        // ��Ÿ���϶��� �ļ����� �о���ߵ�

        if (controller.Distance > range) return false;

        if (remainTime >= 0) return false;

        return true;
    }
    public override void OnBehaviour()
    {
        if (CheckBehaviour()) 
        {
            rb2D.velocity = Vector3.zero;
            remainTime = coolTime;
            CreateProjectile(enemySO);
            
        }
        
        controller.enemyBehaviours.Dequeue();
        controller.enemyBehaviours.Enqueue(this);
    }
    private void CreateProjectile(RangedAttackData rangedAttackData)
    {
        
        _projectileManager.ShootBullet(
                projectileSpawnPosition.position,
                controller.Direction,
                rangedAttackData
                );
    }

 
}
