using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : EnemyBehaviour
{

    private HealthController _healthSystem;
    protected override void Awake()
    {
        base.Awake();
        enemyState = EnemyState.Dead;
        _healthSystem = GetComponent<HealthController>();

    }
    protected override void Start()
    {
        base.Start();
        _healthSystem.OnDeath += OnBehaviour;
    }

    public override void OnBehaviour()
    {
        controller.Rb2D.velocity = Vector3.zero;
        animationController.Death();
        Destroy(gameObject, .35f);

    }
}
