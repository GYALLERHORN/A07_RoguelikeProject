using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Bomb : EnemyBehaviour
{
    [SerializeField][Range(1f, 100f)] public float range;

    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        type = EnemyBehaviourType.Attack;
        Priority = (int)type;
    }

    protected override void Update()
    {
  
    }
    public override void OnBehaviour()
    {
        Destroy(gameObject);
    }

    public override bool CheckBehaviour()
    {
        if (remainTime > 0) return false;

        if (controller.Distance >= range) return false;
   
        return true;
    }
}
