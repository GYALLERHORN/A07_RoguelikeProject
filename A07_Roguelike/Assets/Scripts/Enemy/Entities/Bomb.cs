using UnityEngine;
public class Bomb : EnemyBehaviour
{
    protected void Start()
    {
        controller.enemyBehaviours.Enqueue(this);
        Ready += OnReady;
        Rest += OnRest;
    }
    private void OnReady()
    {
        state = controller.Distance <= range ? State.Ready : State.Rest;
    }
    private void OnRest()
    {
        OnReady();
    }
    public override void OnBehaviour()
    { 
        controller.Rb2D.velocity = Vector3.zero;
        animationController.Death();
        transform.localScale = Vector3.one * 2f;
        Destroy(gameObject, .35f);
        controller.ReInsert();
    }

    [SerializeField][Range(0f, 20f)] private float range;
}
