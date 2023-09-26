using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : EnemyBehaviour
{
    [SerializeField][Range(1f, 100f)] public float range;
    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        type = EnemyBehaviourType.Attack;
        Priority = (int)type;
    }

    public override void OnBehaviour()
    {
        // 투사체 발사
    }

    public override bool CheckBehaviour()
    {
        if (controller.Distance <= range)
        {
            return true;
        }

        return false;
    }
}
