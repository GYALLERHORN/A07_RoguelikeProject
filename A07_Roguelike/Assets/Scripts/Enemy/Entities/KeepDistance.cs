using UnityEngine;

public class KeepDistance : EnemyBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        enemyState = EnemyState.Skill;

    }
    protected override void Start()
    {
        base.Start();
        Rest += OnRest;
        CoolTime += OnCoolTime;
    }
    public override void OnBehaviour()
    {
        Vector2 direction = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * -controller.Direction * runAwaySpeed;
        controller.Rb2D.velocity = direction;
        animationController.Move(direction);
        state = State.Using;

        if (controller.Distance > targetDistance)
        {
            remainTime = coolTime;
            controller.state = EnemyState.Move;
            state = State.CoolTime;
            controller.ReInsert(enemyState);

        }

    }
    private void OnRest()
    {
        if ((controller.state == EnemyState.Move) && CheckCondition())
        {
            controller.state = enemyState;
            state = State.Ready;
        }
    }
    private void OnCoolTime()
    {
        remainTime -= Time.deltaTime;
        if (remainTime < 0f)
        {
            state = State.Rest;
        }
    }

    private bool CheckCondition()
    {
        return controller.Distance < range;
    }

    [SerializeField][Range(0f, 100f)] protected float targetDistance;
    [SerializeField][Range(0f, 100f)] float runAwaySpeed;
    [SerializeField][Range(0f, 100f)] float remainTime;
    [SerializeField][Range(0f, 100f)] float coolTime;
    [SerializeField][Range(0f, 100f)] float range;
}
