using System;
using Unity.VisualScripting;
using UnityEngine;

public class Chase : EnemyBehaviour
{
    private int _priority = 1;


    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    { 
        Priority = _priority;
        CheckBehaviour += CheckChase;
        OnBehaviour += OnChase;
    }

    private void OnChase()
    {
        controller.rb2D.velocity = controller.speed * controller.Direction;
    }

    private bool CheckChase()
    {
        float distance = controller.Distance;

        if (distance < controller.followRange)
        {
          return true;
            
        }
        return false;
    }
}
