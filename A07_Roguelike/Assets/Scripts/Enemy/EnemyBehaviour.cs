using System;
using System.Xml;
using UnityEngine;

public enum EnemyBehaviourType
{
    Look,
    Move,
    Attack,
    Skill,
}

// Enemy의 행동을 정의할 클래스인데.

// EnemyController를 통해서 Enemy를 컨트롤한다

// EnemyBehaviour들은 Enemy의 행동을 정의

// Start() 에서 모든 Behaviour들은 Controller의 Queue에 들어가게된다.

// Controller에서는 FixedUpdate()마다 Queue의 Peek()을 실행 시킨다.

// Move는 항상
public enum State
{
    Ready,
    // Ready는 항상 0번이여아됨
    Rest,
    Using,
    CoolTime,

}

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected EnemyAnimationController animationController;
    protected EnemyController controller;

    public State state;

    protected virtual void Awake()
    {
        animationController = GetComponent<EnemyAnimationController>();
        controller = GetComponent<EnemyController>();
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
