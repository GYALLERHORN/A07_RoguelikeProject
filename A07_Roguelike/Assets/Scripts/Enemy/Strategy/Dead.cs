using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : EnemyBehaviour, IBehaviour
{
    private Rigidbody2D _rb2D;

    private StrategyState _state = StrategyState.Rest;
    private StratgeyType _type = StratgeyType.Dead;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }

    private HealthController _healthSystem;

    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<HealthController>();
        _rb2D = GetComponent<Rigidbody2D>();

    }
    protected override void Start()
    {
        base.Start();
        _healthSystem.OnDeath += OnAction;
    }

    public void OnRest() { }
    public void OnAction() 
    {
        _rb2D.velocity = Vector2.zero;
        animationController.Move(Vector2.zero);
        animationController.Death();
        Destroy(gameObject, 0.35f);
        EndAction(this);
    }

    public void OffAction()
    {

    }

    public void OnCoolTime() { }

}
