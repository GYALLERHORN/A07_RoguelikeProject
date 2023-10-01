using UnityEngine;

public class LongDistanceAttack : EnemyBehaviour, IBehaviour
{
    private StrategyState _state = StrategyState.CoolTime;
    private StratgeyType _type = StratgeyType.Attack;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; set => _type = value; }

    private void Start()
    {
        _projectileManager = ProjectileManager.instance;
    }
    public void OnRest()
    {
        if (!CheckCondition()) return;

        switch (CurrentBehaviourType())
        {
            case null:
            case StratgeyType.Move:
                StartAction(this);
                break;
            default:
                break;

        }
    }
    public void OnAction() 
    {
        remainTime = coolTime;
        CreateProjectile(statsSO);
        EndAction(this);
        
    }
    public void OnCoolTime()
    {
        remainTime -= Time.deltaTime;
        if (remainTime < 0f)
        {
            State = StrategyState.Rest;
        }
    }  

    private void CreateProjectile(RangedAttackData rangedAttackData)
    { 
        _projectileManager.ShootBullet(
                projectileSpawnPosition.position,
                Direction,
                rangedAttackData
                );
    }
    private bool CheckCondition()
    {
        return Distance < range;
    }

    [SerializeField] private RangedAttackData statsSO;
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField][Range(1f, 100f)] private float remainTime;
    [SerializeField][Range(1f, 100f)] private float coolTime;
    [SerializeField][Range(1f, 100f)] private float range;
    private ProjectileManager _projectileManager;


}
