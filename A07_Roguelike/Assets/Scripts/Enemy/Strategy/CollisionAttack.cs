using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionAttack : EnemyBehaviour, IBehaviour
{
    private StrategyState _state = StrategyState.Rest;
    private StratgeyType _type = StratgeyType.Attack;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }
    public void OnRest() { }
    public void OnAction() { }
    public void OnCoolTime() { }
    public void OffAction() { }


    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (CurrentBehaviourType() == StratgeyType.Skill) return;

        remainTime -= Time.deltaTime;
        GameObject go = collision.gameObject;
        if (go == null) return;

        if (go.CompareTag("Player"))
        {
            if (remainTime > 0)
            {
                return;
            }
            HealthController hc = go.GetComponent<HealthController>();

            if (hc == null) return;

            hc.ChangeHealth(-(int)stats.attackSO.power);
            remainTime = delay;
        }
    }
    

    [SerializeField][Range(0f, 20f)] private float delay;
    [SerializeField][Range(0f, 20f)] private float remainTime;

}