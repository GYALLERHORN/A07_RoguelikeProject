using UnityEngine;

public class KeepDistance : EnemyBehaviour
{
    // Ÿ���� Range ���� �ȿ� ���´ٸ� 

    // targetDistnae ��ŭ �Ÿ��� ������.

    [SerializeField][Range(0f, 100f)] protected float targetDistance;

    [SerializeField][Range(0f, 100f)] float runAwaySpeed;

    [SerializeField][Range(0f, 100f)] float remainTime;
    [SerializeField][Range(0f, 100f)] float coolTime;
    [SerializeField][Range(0f, 100f)] float range;

    private float tempSpeed;

    protected override void Start()
    {
        base.Start();
        tempSpeed = enemySO.speed;
        controller.enemyBehaviours.Enqueue(this);

    }
    protected override void Update()
    {
        base.Update();
        remainTime -= Time.deltaTime;
    }

    // �ߵ� ������ üũ�ؼ� �����ϰ�
    // ������ ������ üũ�ؼ� ������.

    // ������ ���� ��Ű�� ���ϸ� �ļ����� �о��ش�.
    public override void OnBehaviour()
    {

        if (!CheckBehaviour()) 
        {
            controller.enemyBehaviours.Dequeue();
            controller.enemyBehaviours.Enqueue(this);
            return;
        }
        
        // Todo ������ �� �÷��̾� �ݴ밡 �ƴ϶� �� �پ��ϰ�
        Vector2 direction = Quaternion.Euler(0,0,Random.Range(-10f,10f)) * -controller.Direction * enemySO.speed;
        rb2D.velocity = direction;
        animationController.Move(direction);

        
    }

    public override bool CheckBehaviour()
    {
        // ��Ÿ���̶�� �ļ����� �о��ش�.
        if (remainTime > 0f) return false;

        float distance = controller.Distance;

        if (IsReady)
        {
            // ��������
            // ��ǥ �Ÿ����� �Ÿ��� ������ �����ӵ��� ���ƿ�
            if (controller.Distance > targetDistance)
            {
                enemySO.speed = tempSpeed;
                IsReady = false;
                remainTime = coolTime;
                return false;
            }
            return true;
            

        }
        else
        {
            // ��������
            // �÷��̾ ������������ ������ �ٰ�����
            if (distance < range)
            {
                IsReady = true;
                enemySO.speed = runAwaySpeed;
                
                return true;
            }
            return false;
        }
        
        
    }
}
