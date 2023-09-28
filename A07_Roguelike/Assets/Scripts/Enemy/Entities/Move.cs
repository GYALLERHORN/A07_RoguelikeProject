using Unity.VisualScripting;
using UnityEngine;

public class Move : EnemyBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        enemyState = EnemyState.Move;
    }
    protected override void Start()
    {
        base.Start();
        Rest += OnRest;
        Using += OnUsing;
    }
    private void OnRest()
    {
        float distance = controller.Distance;
        if (controller.state == EnemyState.Move && attackRange < distance && distance < followRange)
        {
            state = State.Ready;
        }
    }

    private void OnUsing()
    {
        float distance = controller.Distance;
        if (!(attackRange < distance && distance < followRange))
        {
            controller.StopEnemy();
            state = State.Rest;
        }
    }

    public override void OnBehaviour()
    {
        state = State.Using;
        Vector2 direction = controller.Direction;
        controller.Rb2D.velocity = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * direction * speed;
        animationController.Move(direction);
        controller.ReInsert(enemyState);
    }

    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float speed;
}


