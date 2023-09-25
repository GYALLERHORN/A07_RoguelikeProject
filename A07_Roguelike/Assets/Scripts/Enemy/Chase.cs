using System;
using UnityEngine;

public class Chase : EnemyBehaviour
{
    private int _priority = 1;
    [SerializeField][Range(0f, 100f)] private float followRange;
    [SerializeField][Range(0f, 100f)] private float speed;

    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    { 
        Priority = _priority;
        CheckBehaviour += CheckChase;
        OnBehaviour += OnChase;
    }

    private void OnChase()
    {
        controller.rb2D.velocity = speed * controller.Direction;
    }

    private bool CheckChase()
    {
        if (controller.Distance < followRange ) return true;

        return false;
    }
}
