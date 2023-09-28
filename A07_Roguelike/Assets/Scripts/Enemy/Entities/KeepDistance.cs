using UnityEngine;

public class KeepDistance : EnemyBehaviour
{
    protected void Start()
    {
        controller.enemyBehaviours.Enqueue(this);
        Ready += OnReady;
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
        if (controller.Distance > targetDistance)
        {
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
}
