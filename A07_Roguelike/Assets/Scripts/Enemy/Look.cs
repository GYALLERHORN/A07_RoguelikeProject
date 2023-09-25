using UnityEngine;

public class Look : EnemyBehaviour
{
    private int _priority = 0;
    protected void Start()
    {
        Priority = _priority;
        CheckBehaviour += CheckLook;
        OnBehaviour += OnLook;
    }

    private void OnLook()
    {
        Vector2 direction = controller.Direction;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        controller.spriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }

    private bool CheckLook()
    {
        return true;
    }
}
