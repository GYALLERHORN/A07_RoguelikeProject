using System;
using UnityEngine;

public enum State
{   
    Rest,
    Ready,
    Start,
    Using,
    CoolTime,

}

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected EnemyAnimationController animationController;
    protected EnemyController controller;

    public EnemyState enemyState;
    public State state;

    protected virtual void Awake()
    {
        animationController = GetComponent<EnemyAnimationController>();
        controller = GetComponent<EnemyController>();
    }

    protected virtual void Start()
    {
        state = State.Rest;
    }
    protected virtual void Update()
    {
        switch (state)
        {
            case State.Ready:
                Ready?.Invoke();
                break;
            case State.Rest:
                Rest?.Invoke();
                break;
            case State.Using:
                Using?.Invoke();
                break;
            case State.CoolTime:
                CoolTime?.Invoke();
                break;
            default:
                break;
        }
    }
    protected Action Ready;
    protected Action Rest;
    protected Action Using;
    protected Action CoolTime;
    public abstract void OnBehaviour();
}
