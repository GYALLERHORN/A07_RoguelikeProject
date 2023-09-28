using System;
using UnityEngine;

public enum State
{   
    Rest, // 대기 상태
    Ready,// 조건을 만족한 상태 
    Using,// 실행 중인 상태 (Using은 해당 Behaviour 작동이 끝날때까지 유지됨)
    CoolTime, // 쿨타임

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
