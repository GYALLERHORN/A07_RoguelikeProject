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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (State == StrategyState.Action)
        {
            GameObject go = collision.gameObject;
            if (go == null) return;

            if (go.CompareTag("Player"))
            {
                HealthController hc = go.GetComponent<HealthController>();

                if (hc == null) return;

                hc.ChangeHealth(-damage);
            }
        }
    }

    [SerializeField][Range(0, 20)] private int damage;

}
