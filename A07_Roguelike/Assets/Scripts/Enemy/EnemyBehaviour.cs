using System;
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
