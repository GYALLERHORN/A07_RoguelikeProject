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

// EnemyBehaviour들은 하나의 어떤 기능을 나타내는 클래스 ex) 특정한 행동, 스킬 -> 이동은 제외

// 컨트롤러에서는 평소에는 그냥 이동을 하다가

// EnemyBehaviour들은 조건을 만족하면 Queue에 추가하고

// Contorller에서는 Queue에 뭔가 들어오면 실행시킨다. 

[Serializable]
public abstract class EnemyBehaviour : MonoBehaviour
{
    protected EnemyController controller;
    protected Animator animator;
    protected bool IsReady;

    [SerializeField][Range(1f, 100f)] public float range;
    [SerializeField][Range(1f, 100f)] public float coolTime;
    [SerializeField] public float remainTime = 0;

    protected virtual void Awake()
    {
        controller = GetComponent<EnemyController>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        if (remainTime >= 0)
        {
            remainTime -= Time.deltaTime;
        }
        
    }

    public abstract void OnBehaviour();
    public abstract bool CheckBehaviour();
}
