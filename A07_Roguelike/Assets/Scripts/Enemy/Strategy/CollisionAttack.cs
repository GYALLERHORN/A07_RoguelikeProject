using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttack : EnemyBehaviour, IBehaviour
{
    private StrategyState _state = StrategyState.Rest;
    private StratgeyType _type = StratgeyType.Skill;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }
    public void OnRest() { }
    public void OnAction() { }
    public void OnCoolTime() { }
    public void OffAction() { }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (remainTime > 0)
        {
            remainTime -= Time.deltaTime;
            return;
        }
        
        GameObject go = collision.gameObject;
        if (go == null) return;

        if (go.CompareTag("Player"))
        {
            HealthController hc = go.GetComponent<HealthController>();

            if (hc == null) return;

            hc.ChangeHealth(-(int)StatData.power);
            remainTime = delay;
        }
    }

    [SerializeField][Range(0f, 20f)] private float delay;
    [SerializeField][Range(0f, 20f)] private float remainTime;

}