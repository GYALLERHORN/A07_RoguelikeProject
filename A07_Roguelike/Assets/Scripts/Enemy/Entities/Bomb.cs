using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UIElements;

public class Bomb : EnemyBehaviour
{
    [SerializeField] [Range(0f,20f)] private float range;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        controller.enemyBehaviours.Enqueue(this);
    }

    protected override void Update()
    {
        base.Update();
    }
    public override void OnBehaviour()
    {
        if (CheckBehaviour())
        {
            rb2D.velocity = Vector3.zero;
            controller.isDead = true;
            animationController.Death();
            transform.localScale = Vector3.one * 1.5f;
            Destroy(gameObject, .35f);
        }
        controller.enemyBehaviours.Dequeue();
        controller.enemyBehaviours.Enqueue(this);


    }

    public override bool CheckBehaviour()
    {

        if (controller.Distance > range) return false;
   
        return true;
    }
}
