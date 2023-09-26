using UnityEngine;

public class Look : EnemyBehaviour
{
    protected void Start()
    {
        type = EnemyBehaviourType.Look;
        Priority = (int)type;
    }
    public override void OnBehaviour()
    {
        Vector2 direction = controller.Direction;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        controller.spriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }

    public override bool CheckBehaviour()
    {
        return true;
    }
}
