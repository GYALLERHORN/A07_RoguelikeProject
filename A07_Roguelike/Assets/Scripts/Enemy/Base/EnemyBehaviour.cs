using System;
using UnityEngine;

[Serializable]
public abstract class EnemyBehaviour : MonoBehaviour
{
    protected GameObject Target; // 임시
    protected EnemyAnimationController animationController;
    protected EnemyBehaviourController behaviourController;

    public Vector2 Direction { get { return (Target.transform.position - transform.position).normalized; } }
    public float Distance { get { return Vector3.Distance(transform.position, Target.transform.position); } }

    protected virtual void Awake()
    {
        Target = GameObject.Find("Player"); // 임시
        animationController = GetComponent<EnemyAnimationController>();
        behaviourController = GetComponent<EnemyBehaviourController>();
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
        behaviour.State = StrategyState.CoolTime;
        behaviourController.CurrentBehaviour = null;
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
