using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : EnemyBehaviour
{
    private int _priority = 2;
    [SerializeField][Range(1f, 100f)] public float range;
    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        Priority = _priority;
        CheckBehaviour += CheckAtack;
        OnBehaviour += OnAttack;
    }

    private void OnAttack()
    {
        // 투사체 발사
    }

    private bool CheckAtack()
    {
        if (controller.Distance <= range)
        {
            return true;
        }

        return false;
    }
}
