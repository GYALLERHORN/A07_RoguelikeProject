using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


   public class EnemyController : MonoBehaviour
{
    [SerializeField] public GameObject Target; // юс╫ц
    public bool isDead = false;

    public Vector2 Direction { get { return (Target.transform.position - transform.position).normalized; } }
    public float Distance { get { return  Vector3.Distance(transform.position, Target.transform.position); } }
    [SerializeField]public Queue<EnemyBehaviour> enemyBehaviours { get; private set; }

    public CharacterStatsHandler statsHandler { get; private set; }
     
    protected virtual void Awake()
    {
        enemyBehaviours = new Queue<EnemyBehaviour>();
        statsHandler = GetComponent<CharacterStatsHandler>();

    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {

    }

    protected void FixedUpdate()
    {
        if (enemyBehaviours.Count > 0)
        {
            enemyBehaviours.Peek().OnBehaviour();
        }
    }


}
