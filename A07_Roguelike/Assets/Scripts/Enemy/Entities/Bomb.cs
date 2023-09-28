using UnityEngine;
public class Bomb : EnemyBehaviour
{
    private HealthController _healthSystem;
    protected override void Awake()
    {
        base.Awake();
        enemyState = EnemyState.Skill;
        _healthSystem = GetComponent<HealthController>();

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
        transform.localScale *= size;
        _healthSystem.ChangeHealth(-int.MaxValue);
    }

    [SerializeField][Range(0f, 20f)] private float range;
    [SerializeField][Range(0f, 20f)] private float size;
}
