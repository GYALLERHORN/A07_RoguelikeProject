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
    public int Priority { get; protected set; }

    protected EnemyController controller;

    protected virtual void Awake()
    {
        controller = GetComponent<EnemyController>();
    }

    public abstract void OnBehaviour();
    public abstract bool CheckBehaviour();
}
