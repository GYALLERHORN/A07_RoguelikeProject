using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : EnemyBehaviour
{
    [SerializeField] private Transform projectileSpawnPosition;
    private ProjectileManager _projectileManager;

    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        type = EnemyBehaviourType.Attack;
        Priority = (int)type;
        _projectileManager = ProjectileManager.instance;
    }

    protected override void Update()
    {
        base.Update();

        if (remainTime < 0 && !IsReady && CheckBehaviour())
        {
            controller.enemyBehaviours.Enqueue(this);
            IsReady = true;
        }
    }
    public override bool CheckBehaviour()
    {
        if (controller.Distance > range) return false;

        return true;
    }
    public override void OnBehaviour()
    {
        if (remainTime >= 0) 
        {
            controller.enemyBehaviours.Dequeue();
            controller.enemyBehaviours.Enqueue(this);
            return; 
        }
        
        remainTime = coolTime;
        RangedAttackData rangedAttackData = controller.GetAttakSO() as RangedAttackData;
        float projectilesAngleSpace = rangedAttackData.multipleProjectilesAngle;
        int numberOfProjectilesPerShot = rangedAttackData.numberofProjectilesPerShot;

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * rangedAttackData.multipleProjectilesAngle;

        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-rangedAttackData.spread, rangedAttackData.spread);
            angle += randomSpread;
            CreateProjectile(rangedAttackData, angle);
        }

        controller.enemyBehaviours.Dequeue();
        IsReady = false;

        if (CheckBehaviour())
        {
            controller.enemyBehaviours.Enqueue(this);
            IsReady = true;
        }

    }
    private void CreateProjectile(RangedAttackData rangedAttackData, float angle)
    {
        
        _projectileManager.ShootBullet(
                projectileSpawnPosition.position,
                RotateVector2(controller.Direction, angle),
                rangedAttackData
                );
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
 
}
