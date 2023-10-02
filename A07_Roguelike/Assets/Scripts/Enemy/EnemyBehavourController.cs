using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class EnemyBehaviourController : MonoBehaviour
{
    [SerializeField] public IBehaviour CurrentBehaviour { get; set; }
    public Queue<IBehaviour> BehaviourQueue { get; private set; }

    private void Awake()
    {
        
    }

    private void Start()
    {
        BehaviourQueue = new Queue<IBehaviour>();

        foreach(IBehaviour behaviour in GetComponents<IBehaviour>())
        {
            BehaviourQueue.Enqueue(behaviour);
        }
    }
    private void Update()
    {
        BehaviourUpdate();
        CurrentBehaviour?.OnAction();
    }

    private void FixedUpdate()
    {
        
    }

    // Action
    private void BehaviourUpdate()
    {
        Queue<IBehaviour> tempQueue = new Queue<IBehaviour>();

        while(BehaviourQueue.Count > 0) 
        {
            IBehaviour behaviour = BehaviourQueue.Dequeue();

            switch (behaviour.State)
            {
                case StrategyState.Rest:
                    behaviour.OnRest();
                    break;
                case StrategyState.Ready:
                    break;
                case StrategyState.Action:
                    break;
                case StrategyState.CoolTime:
                    behaviour.OnCoolTime();
                    break;
                default:
                    break;
            }

            tempQueue.Enqueue(behaviour);
        }
        BehaviourQueue = tempQueue;
    }
}