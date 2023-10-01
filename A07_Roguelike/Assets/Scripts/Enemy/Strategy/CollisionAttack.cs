using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttack : EnemyBehaviour, IBehaviour
{
    private StrategyState _state = StrategyState.Rest;
    private StratgeyType _type = StratgeyType.Skill;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }
    public void OnBehaviour() { }
    public void OnRest() { return; }
    public void OnReady() { }
    public void OnAction() { }
    public void OnCoolTime() { }
}
