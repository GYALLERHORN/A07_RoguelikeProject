using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;


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
    public Queue<EnemyBehaviour> enemyBehaviours { get; private set; }

    protected virtual void Awake()
    {
        enemyBehaviours = new Queue<EnemyBehaviour>();
        StatsHandler = GetComponent<CharacterStatsHandler>();
        Rb2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Collider = GetComponentInChildren<Collider2D>();
        AnimationController = GetComponent<EnemyAnimationController>();

    }
    protected void FixedUpdate()
    {
        StopEnemy();
        if (enemyBehaviours.Count > 0)
        {
            EnemyBehaviour peekBehaviour = enemyBehaviours.Peek();

            if (peekBehaviour.state == State.Ready || peekBehaviour.state == State.Using)
            {
                enemyBehaviours.Peek().OnBehaviour();
            }
            else
            {
                ReInsert();
            }
            
        }
    }

    public void StopEnemy()
    {
        Rb2D.velocity = Vector3.zero;
        AnimationController.Move(Vector2.zero);
    }
    public void ReInsert()
    {
        EnemyBehaviour peekBehaviour = enemyBehaviours.Dequeue();
        enemyBehaviours.Enqueue(peekBehaviour);
    }

}
