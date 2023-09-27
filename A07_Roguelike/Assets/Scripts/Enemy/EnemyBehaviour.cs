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

// Enemy�� �ൿ�� ������ Ŭ�����ε�.

// EnemyController�� ���ؼ� Enemy�� ��Ʈ���Ѵ�

// EnemyBehaviour���� Enemy�� �ൿ�� ����

// Start() ���� ��� Behaviour���� Controller�� Queue�� ���Եȴ�.

// Controller������ FixedUpdate()���� Queue�� Peek()�� ���� ��Ų��.

// Move�� �׻�
public enum State
{
    Ready,
    // Ready�� �׻� 0���̿��Ƶ�
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
