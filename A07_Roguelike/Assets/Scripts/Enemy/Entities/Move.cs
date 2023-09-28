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
    }

    private bool CheckCondition()
    {
        float distance = controller.Distance;
        return attackRange < distance && distance < followRange ? true : false;
    }

    // Update���� ���Ǵ� �޼���
    private void OnRest()
    {
        if (controller.state == EnemyState.Move && CheckCondition())
        {
            state = State.Ready;
        }
    }

    // FixedUpdate���� ���Ǵ� �޼���
    public override void OnBehaviour()
    {
        state = State.Using;
        Vector2 direction = controller.Direction;
        controller.Rb2D.velocity = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * direction * speed;
        animationController.Move(direction);
        controller.ReInsert(enemyState);

        float distance = controller.Distance;
        if (!CheckCondition())
        {
            controller.StopEnemy();
            state = State.Rest;
        }
    }
    

    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float speed;
}


