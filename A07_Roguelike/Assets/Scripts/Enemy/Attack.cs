using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EnemyBehaviour
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
    }

    public override void OnBehaviour()
    {
        Destroy(gameObject);
    }

    public override bool CheckBehaviour()
    {
        if (controller.Distance < range)
        {
            return true;
        }

        return false;
    }
}
