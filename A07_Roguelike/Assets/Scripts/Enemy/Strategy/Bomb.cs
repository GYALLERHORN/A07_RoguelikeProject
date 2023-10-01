using UnityEngine;
public class Bomb : EnemyBehaviour, IBehaviour
{
    private StrategyState _state = StrategyState.Rest;
    private StratgeyType _type = StratgeyType.Skill;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }

    private HealthController _healthSystem;
    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<HealthController>();

    }
   
    public void OnRest()
    {
        if (!(Distance < range)) return;
        
        switch (CurrentBehaviourType())
        {
            default:
                StartAction(this);
                break;
        }
    }
    public void OnAction() 
    {
        transform.localScale *= size;
        _healthSystem.ChangeHealth(-int.MaxValue);
        EndAction(this);
    }
    public void OnCoolTime() { }

    [SerializeField][Range(0f, 20f)] private float range;
    [SerializeField][Range(0f, 20f)] private float size;
    [SerializeField][Range(0, 20)] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (State == StrategyState.Action)
        {
            GameObject go = collision.gameObject;
            if (go == null) return;

            if (go.CompareTag("Player"))
            {
                HealthController hc = go.GetComponent<HealthController>();

                if (hc == null) return;

                hc.ChangeHealth(-damage);

            }
        }
    }
}

        
