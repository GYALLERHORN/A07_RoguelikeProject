using UnityEngine;

public class KeepDistance : EnemyBehaviour
{
    enum KeepDistanceState
    {
        Rest,
        Using,
    }
    protected void Start()
    {
        _enemySO = (EnemySO)controller.StatsHandler.CurrentStates.attackSO;
        tempSpeed = _enemySO.speed;
        controller.enemyBehaviours.Enqueue(this);
        Ready += OnReady;
        Rest += OnRest;
        Using += OnUsing;
        CoolTime += OnCoolTime;
    }
    public override void OnBehaviour()
    {
        switch (keepDistanceState)
        {
            case KeepDistanceState.Rest:
                Vector2 direction = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * -controller.Direction * _enemySO.speed;
                controller.Rb2D.velocity = direction;
                animationController.Move(direction);
                break;
            case KeepDistanceState.Using:
                if (controller.Distance > targetDistance)
                {
                    _enemySO.speed = tempSpeed;
                    remainTime = coolTime;
                    state = State.CoolTime;
                    keepDistanceState = KeepDistanceState.Rest;
                }
                break;
            default:
                break;
        }
        
    }
    private void OnReady()
    {
        state = controller.Distance < range ? State.Ready : State.Rest;
    }
    private void OnRest()
    {
        OnReady();
    }
    private void OnUsing()
    {
        _enemySO.speed = runAwaySpeed;
        if (controller.Distance > targetDistance)
        {
            _enemySO.speed = tempSpeed;
            remainTime = coolTime;
            state = State.CoolTime;
            controller.ReInsert();
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
    private float tempSpeed;
    KeepDistanceState keepDistanceState = KeepDistanceState.Rest;
    private EnemySO _enemySO;
}
