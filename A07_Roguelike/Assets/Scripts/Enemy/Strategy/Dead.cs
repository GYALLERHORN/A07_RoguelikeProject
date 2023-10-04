using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Dead : EnemyBehaviour, IBehaviour
{
    private Rigidbody2D _rb2D;

    private StrategyState _state = StrategyState.Rest;
    private StratgeyType _type = StratgeyType.Dead;
    public StrategyState State { get => _state; set => _state = value; }
    public StratgeyType Type { get => _type; }

    private HealthController _healthSystem;
    [SerializeField] [Range(0,100)] private int dropPercent;
    [SerializeField] [Range(0f, 5f)] private float delay;


    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<HealthController>();
        _rb2D = GetComponent<Rigidbody2D>();

    }
    protected override void Start()
    {
        base.Start();
        _healthSystem.OnDeath += OnAction;
    }

    public void OnRest() { }
    public void OnAction() 
    {
        _rb2D.velocity = Vector2.zero;
        animationController.Move(Vector2.zero);
        animationController.Death();
        Invoke("DropItem", delay);
        Destroy(gameObject, delay);

        // 아이템을 확률적으로 생성
        // dropitem 
        
        EndAction(this);
    }

    public void DropItem()
    {
        int percent = Random.Range(0, 100);

        if (percent <= dropPercent)
        {
            int randomItem = Random.Range(1, ItemDatabase.Instance.itemDB.Count);
            if (percent < 5)
            {
                randomItem = 0;
            }
            GameObject go = Instantiate(ItemDatabase.Instance.itemPrefab, transform.position, Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(ItemDatabase.Instance.itemDB[randomItem]);
        }
    }

    public void OffAction()
    {

    }

    public void OnCoolTime() { }

}
