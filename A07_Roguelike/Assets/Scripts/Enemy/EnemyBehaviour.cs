using System;
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

[Serializable]
public abstract class EnemyBehaviour : MonoBehaviour
{
    protected EnemySO enemySO;
    protected EnemyAnimationController animationController;
    protected EnemyController controller;
    protected bool IsReady = false;
    public Rigidbody2D rb2D;
    public SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        animationController = GetComponent<EnemyAnimationController>();
        controller = GetComponent<EnemyController>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Start()
    {

        enemySO = (EnemySO)controller.statsHandler.CurrentStates.attackSO;
    }

    protected virtual void Update()
    {
        rb2D.velocity = Vector3.zero;   
    }

    public abstract void OnBehaviour();
    public abstract bool CheckBehaviour();
}
