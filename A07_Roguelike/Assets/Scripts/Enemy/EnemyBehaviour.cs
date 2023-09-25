using System;
using UnityEngine;

public enum EnemyBehaviourType
{
    Look,
    Move,
    Attack,
    Skill,
}

public class EnemyBehaviour : MonoBehaviour
{
    public Func<bool> CheckBehaviour;
    public Action OnBehaviour;
    public int Priority { get; protected set; }

    protected EnemyController controller;

    protected virtual void Awake()
    {
        controller = GetComponent<EnemyController>();
    }

  
}
