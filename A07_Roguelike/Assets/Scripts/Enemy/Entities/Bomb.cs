using UnityEngine;
public class Bomb : EnemyBehaviour
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
    }
    
    private void OnRest()
    {
        if ((controller.state == EnemyState.Move) && controller.Distance <= range)
        {
            controller.state = enemyState;
            state = State.Ready;
        }
    }
    public override void OnBehaviour()
    { 
        controller.Rb2D.velocity = Vector3.zero;
        animationController.Death();
        transform.localScale = Vector3.one * 2f;
        Destroy(gameObject, .35f);
    }

    [SerializeField][Range(0f, 20f)] private float range;
}
