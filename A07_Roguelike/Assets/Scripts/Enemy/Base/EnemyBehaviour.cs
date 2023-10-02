using System;
using UnityEngine;

[Serializable]
public abstract class EnemyBehaviour : MonoBehaviour
{
    protected GameObject Target; // 임시
    protected EnemyAnimationController animationController;
    protected EnemyBehaviourController behaviourController;
    protected CharacterStatsHandler characterStatsHandler;

    public Vector2 Direction { get { return (Target.transform.position - transform.position).normalized; } }
    public float Distance { get { return Vector3.Distance(transform.position, Target.transform.position); } }

    public AttackSO StatData { get { return characterStatsHandler.CurrentStates.attackSO; } } 

    protected virtual void Awake()
    {
        Target = GameObject.Find("Player"); // 임시
        animationController = GetComponent<EnemyAnimationController>();
        behaviourController = GetComponent<EnemyBehaviourController>();
        characterStatsHandler = GetComponent<CharacterStatsHandler>();

    }

    public void StartAction(IBehaviour behaviour)
    {
        IBehaviour prevBehaviour = behaviourController.CurrentBehaviour;
        if (prevBehaviour != null) 
        {
            EndAction(prevBehaviour);
        }
        behaviour.State = StrategyState.Action;
        behaviourController.CurrentBehaviour = behaviour;
    }

    public void EndAction(IBehaviour behaviour)
    {
        behaviour.OffAction();
        behaviourController.CurrentBehaviour = null;
        behaviour.State = StrategyState.CoolTime;
        
    }

    protected StratgeyType? CurrentBehaviourType()
    {
        if (behaviourController.CurrentBehaviour != null)
        {
            return behaviourController.CurrentBehaviour.Type;
        }

        return null;

    }
}
