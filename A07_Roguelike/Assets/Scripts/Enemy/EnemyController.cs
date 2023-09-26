using System.Collections.Generic;
using UnityEngine;


   public class EnemyController : TopDownCharacterController
{
    [SerializeField] public GameObject Target; // юс╫ц
    public bool isDead = false;

    public Rigidbody2D rb2D;
    public SpriteRenderer spriteRenderer;

    public Vector2 Direction { get { return (Target.transform.position - transform.position).normalized; } }
    public float Distance { get { return  Vector3.Distance(transform.position, Target.transform.position); } }
    [SerializeField]public Queue<EnemyBehaviour> enemyBehaviours { get; private set; }


    public CharacterStats GetStats()
    {
        return Stats.CurrentStates;
    }

    public EnemySO GetEnemySO()
    {
        return (EnemySO)GetStats().attackSO;
    }
     
    protected override void Awake()
    {
        base.Awake();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyBehaviours = new Queue<EnemyBehaviour>(); 

    }

    protected void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;
        if (isDead)
        {
            CallMoveEvent(direction);
            Rotate(direction);
            return;
        }
        
        float distance = Distance;
        if (distance < GetEnemySO().followRange && GetEnemySO().attackRange < distance)
        {
            direction = Direction;
        }

        CallMoveEvent(direction);
        Rotate(direction);
        
        if(enemyBehaviours.Count > 0)
        {
            enemyBehaviours.Peek().OnBehaviour();
        }
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteRenderer.flipX = Mathf.Abs(rotZ) > 90;
    }

}
