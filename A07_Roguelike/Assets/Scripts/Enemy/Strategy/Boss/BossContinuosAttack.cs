using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossContinuosAttack : EnemyBehaviour, IBehaviour
{
    // 원거리 공격 패턴,
    // 돌진 패턴,
    // 분열? 패턴,
    // 범위 지정 ( 점프해서 범위를 표시하고 내려찍기)
    // 주변 범위 폭발 패턴
    enum AttackState
    {
        Rest,
        First,
        Second,
        Third,
    }
    private StrategyState _state = StrategyState.CoolTime;
    private StratgeyType _type = StratgeyType.Skill;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }

    private Rigidbody2D _rb2D;

    private void Start()
    {
        _projectileManager = ProjectileManager.instance;
        _rb2D = GetComponent<Rigidbody2D>();
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
    public void OnAction() 
    {
        switch (attackState)
        {
            case AttackState.Rest:
                Init();
                break;
            case AttackState.First:
                Attack();
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
    public void OffAction() 
    {
        remainTime = coolTime;
        attackState = AttackState.Rest;
    }

    public void Init()
    {
        attackState = AttackState.First;
        repeat = 6;
        startAngle = 0;
    }

    public void Attack()
    {
        if (remainTime > 0)
        {
            remainTime -= Time.deltaTime;
            return;
        }

        int shotNum = 12;
        Vector2 direction = Direction;

        if (repeat > 0 && repeat % 2 == 0)
        {
            _rb2D.velocity = Vector2.zero;
            animationController.Move(Vector2.zero);
            animationController.Attack();
            for (int i = 0; i < shotNum; i++)
            {
                _projectileManager.ShootBullet(
                    projectileSpawnPosition.position,
                    Quaternion.Euler(0, 0, startAngle + 30f * i) * direction,
                    statsSO
                    );
            }
            repeat--;
            startAngle += 10;
            remainTime = firstAttackDelay;
        }
        else if (repeat > 0 && repeat % 2 == 1)
        {
            _rb2D.velocity = Direction * speed;
            animationController.Move(Direction * speed);
            remainTime = firstAttackDelay;
            repeat--;
        }
        else
        {
            _rb2D.velocity = Vector2.zero;
            EndAction(this);
        }
    }


    [SerializeField][Range(1f, 100f)] private float remainTime;
    [SerializeField][Range(1f, 100f)] private float coolTime;
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private RangedAttackData statsSO;
    [SerializeField][Range(1f, 100f)] private float range;
    [SerializeField][Range(1f, 100f)] private float speed;
    [SerializeField][Range(1f, 100f)] private float firstAttackDelay;
    private int repeat = 6;
    private float startAngle = 0;
    AttackState attackState = AttackState.Rest;
    private ProjectileManager _projectileManager;

}
