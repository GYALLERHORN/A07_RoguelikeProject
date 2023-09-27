using System.Collections.Generic;
using UnityEngine;


   public class EnemyController : MonoBehaviour
{
    [SerializeField] public GameObject Target; // юс╫ц
    public Vector2 Direction { get { return (Target.transform.position - transform.position).normalized; } }
    public float Distance { get { return  Vector3.Distance(transform.position, Target.transform.position); } }
    public Rigidbody2D Rb2D { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; } 
    public CharacterStatsHandler StatsHandler { get; private set; }
    public Queue<EnemyBehaviour> enemyBehaviours { get; private set; }

    protected virtual void Awake()
    {
        enemyBehaviours = new Queue<EnemyBehaviour>();
        StatsHandler = GetComponent<CharacterStatsHandler>();
        Rb2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }
    protected void FixedUpdate()
    {
        if (enemyBehaviours.Count > 0)
        {
            StopEnemy();

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

    private void StopEnemy()
    {
        Rb2D.velocity = Vector3.zero;
    }
    public void ReInsert()
    {
        EnemyBehaviour peekBehaviour = enemyBehaviours.Dequeue();
        enemyBehaviours.Enqueue(peekBehaviour);
    }

}
