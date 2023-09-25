using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public GameObject Target; // юс╫ц

    [SerializeField] private List<EnemyBehaviour> enemyBehaviours = new List<EnemyBehaviour>();

    [SerializeField][Range(0f, 100f)] public float followRange;
    [SerializeField][Range(0f, 100f)] public float speed;

    public Rigidbody2D rb2D;

    public SpriteRenderer spriteRenderer;

    [SerializeField]private List<EnemyBehaviour> _nextBehaviours = new List<EnemyBehaviour>();


    public Vector2 Direction { get { return (Target.transform.position - transform.position).normalized; } }

    public float Distance { get { return  Vector3.Distance(transform.position, Target.transform.position); } }
        
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        foreach (EnemyBehaviour enemyBehaviour in GetComponents<EnemyBehaviour>())
        {
            enemyBehaviours.Add(enemyBehaviour);
        }

    }

    private void FixedUpdate()
    {
        _nextBehaviours.Clear();
        rb2D.velocity = Vector3.zero;

        foreach (EnemyBehaviour enemyBehaviour in enemyBehaviours)
        {
            if (!enemyBehaviour.CheckBehaviour())
            {
                continue;
            }

            if (enemyBehaviour.Priority == 0)
            {
                enemyBehaviour.OnBehaviour();
                continue;
            }

            if(_nextBehaviours.Count == 0)
            {
                _nextBehaviours.Add(enemyBehaviour);
            }
            else
            {
                if(enemyBehaviour.Priority > _nextBehaviours[0].Priority)
                {
                    _nextBehaviours.Clear();
                    _nextBehaviours.Add(enemyBehaviour);
                }
                else if(enemyBehaviour.Priority == _nextBehaviours[0].Priority)
                {
                    _nextBehaviours.Add(enemyBehaviour);
                }
            }
            
        }

        if (_nextBehaviours.Count != 0)
        {
            _nextBehaviours[Random.Range(0, _nextBehaviours.Count)].OnBehaviour();
        }
        
        
    }
}
