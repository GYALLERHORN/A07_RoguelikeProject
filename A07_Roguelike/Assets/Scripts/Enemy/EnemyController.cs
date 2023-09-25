using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public GameObject Target; // 임시

    [SerializeField] private List<EnemyBehaviour> enemyBehaviours = new List<EnemyBehaviour>();

    public Rigidbody2D rb2D;

    public SpriteRenderer spriteRenderer;

    private EnemyBehaviour _nextBehaviour;
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
        foreach(EnemyBehaviour EnemyBehaviour in enemyBehaviours)
        {
            if (EnemyBehaviour.CheckBehaviour())
            {
                if (EnemyBehaviour.Priority == 0)
                {
                    EnemyBehaviour.OnBehaviour();
                    continue;
                }

                if(_nextBehaviour == null)
                {
                    _nextBehaviour = EnemyBehaviour;
                }
                else
                {
                    _nextBehaviour = EnemyBehaviour.Priority > _nextBehaviour.Priority ? EnemyBehaviour : _nextBehaviour;
                }
            }
        }

        if (_nextBehaviour != null)
        {
            _nextBehaviour.OnBehaviour();
        }


        // 만약 여러개의 우선순위가 겹친다면 그중에서 랜덤으로 선택??
    }
}
