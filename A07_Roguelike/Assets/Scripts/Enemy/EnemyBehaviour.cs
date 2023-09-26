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

// EnemyBehaviour���� �ϳ��� � ����� ��Ÿ���� Ŭ���� ex) Ư���� �ൿ, ��ų -> �̵��� ����

// ��Ʈ�ѷ������� ��ҿ��� �׳� �̵��� �ϴٰ�

// EnemyBehaviour���� ������ �����ϸ� Queue�� �߰��ϰ�

// Contorller������ Queue�� ���� ������ �����Ų��. 

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
