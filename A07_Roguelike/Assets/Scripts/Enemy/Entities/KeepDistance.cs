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
        Using += OnUsing;
        CoolTime += OnCoolTime;
    }
    public override void OnBehaviour()
    {
        Vector2 direction = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * -controller.Direction * runAwaySpeed;
        controller.Rb2D.velocity = direction;
        animationController.Move(direction);
        state = State.Using;
                  
    }
    private void OnRest()
    {
        if ((controller.state == EnemyState.Move) && controller.Distance < range)
        {
            controller.state = enemyState;
            state = State.Ready;
        }
    }
    private void OnUsing()
    {
        if (controller.Distance > targetDistance)
        {
            remainTime = coolTime;
            controller.state = EnemyState.Move;
            state = State.CoolTime;
            controller.ReInsert(enemyState);
            
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

    [SerializeField][Range(0f, 100f)] protected float targetDistance;
    [SerializeField][Range(0f, 100f)] float runAwaySpeed;
    [SerializeField][Range(0f, 100f)] float remainTime;
    [SerializeField][Range(0f, 100f)] float coolTime;
    [SerializeField][Range(0f, 100f)] float range;
}
