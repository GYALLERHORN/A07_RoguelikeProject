using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public enum EnemyState
{
    Move,
    Hurt,
    Attack,
    Skill,
    Dead,
    EndPoint,
}

public class EnemyController : MonoBehaviour
{

    [SerializeField] public GameObject Target; // юс╫ц

    [SerializeField] private CharacterStats Stats;
    public Vector2 Direction { get { return (Target.transform.position - transform.position).normalized; } }
    public float Distance { get { return  Vector3.Distance(transform.position, Target.transform.position); } }

    public Rigidbody2D Rb2D { get; private set; }
    public Collider2D Collider { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; } 
    public CharacterStatsHandler StatsHandler { get; private set; }
    public EnemyAnimationController AnimationController { get; private set; }

    
    Dictionary<EnemyState, Queue<EnemyBehaviour>> enemyBehavioursDic = new Dictionary<EnemyState, Queue<EnemyBehaviour>>();

    public EnemyState state = EnemyState.Move;
    public Queue<EnemyBehaviour> enemyBehaviours { get; private set; }

    protected virtual void Awake()
    {
        StatsHandler = GetComponent<CharacterStatsHandler>();
        Rb2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Collider = GetComponentInChildren<Collider2D>();
        AnimationController = GetComponent<EnemyAnimationController>();
    }

    protected void Start()
    {
        for (int i = 0; i < (int)EnemyState.EndPoint; i++)
        {
            enemyBehavioursDic.Add((EnemyState)i, new Queue<EnemyBehaviour>());
        }

        foreach (EnemyBehaviour enemyBehaviour in GetComponents<EnemyBehaviour>())
        {
            enemyBehavioursDic[enemyBehaviour.enemyState].Enqueue(enemyBehaviour);
        }
    }
    protected void FixedUpdate()
    {
        OnBehaviour(state);
    }

    private void OnBehaviour(EnemyState state)
    {
        Queue<EnemyBehaviour> enemyBehaviours = enemyBehavioursDic[state];
        if (enemyBehaviours.Count > 0)
        {
            EnemyBehaviour peekBehaviour = enemyBehaviours.Peek();

            if (peekBehaviour.state == State.Ready || peekBehaviour.state == State.Using)
            {
                enemyBehaviours.Peek().OnBehaviour();
            }
            else
            {
                ReInsert(state);
            }

        }
    }


    public void StopEnemy()
    {
        Rb2D.velocity = Vector3.zero;
        AnimationController.Move(Vector2.zero);
    }
    public void ReInsert(EnemyState state)
    {
        EnemyBehaviour peekBehaviour = enemyBehavioursDic[state].Dequeue();

        enemyBehavioursDic[state].Enqueue(peekBehaviour);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;

    }



}
