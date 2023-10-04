using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class EnemyBehaviourController : MonoBehaviour
{
    public IBehaviour CurrentBehaviour { get; set; }
    
    [SerializeField] public List<IBehaviour> BehavioursList = new List<IBehaviour>();
    [SerializeField] public List<EnemyBehaviour> BehavioursList2 = new List<EnemyBehaviour>();

    private void Start()
    {
        foreach (IBehaviour behaviour in GetComponents<IBehaviour>())
        {
            BehavioursList.Add(behaviour);
        }
    }
    private void Update()
    {
        BehaviourUpdate();
        CurrentBehaviour?.OnAction();
    }

    // Action
    private void BehaviourUpdate()
    {
        List<IBehaviour> temp = new List<IBehaviour>();

        while(BehavioursList.Count > 0) 
        {
            IBehaviour behaviour = BehavioursList[0];
            BehavioursList.RemoveAt(0);

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

            temp.Add(behaviour);
        }
        BehavioursList = temp;
    }
}