using System;
using UnityEngine;

public enum EnemyBehaviourType
{
    Look,
    Move,
    Attack,
    Skill,
}

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected EnemyBehaviourType type;
    public int Priority { get; protected set; }

    protected EnemyController controller;
    protected Animator animator;

    protected virtual void Awake()
    {
        controller = GetComponent<EnemyController>();
        animator = GetComponentInChildren<Animator>();
    }

    public abstract void OnBehaviour();
    public abstract bool CheckBehaviour();
}
