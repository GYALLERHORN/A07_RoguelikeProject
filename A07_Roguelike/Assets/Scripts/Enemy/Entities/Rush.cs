using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rush : EnemyBehaviour
{
    // On�Ǹ�

    // �����ð����� �÷��̾��� ��ġ�� ���� (�̋� ������ ǥ�õȴ�.)

    // �����ð� ���� �ش� �������� ����.



    [SerializeField] private GameObject rushRange;
    Vector2 targetPos = Vector2.zero; 

    [SerializeField][Range(0f, 100f)] float range;

    [SerializeField][Range(0f, 100f)] float coolTime;
    float remainTime;

    [SerializeField][Range(0f, 100f)] float chargingTime;
    [SerializeField][Range(0f, 100f)] float remainchargingTime;
    [SerializeField][Range(0f, 100f)] float speed;

    private bool isReady = false;
    private bool isRush = false;



    protected override void Start()
    {
        base.Start();
        controller.enemyBehaviours.Enqueue(this);
        remainTime = Random.Range(0f, coolTime);
    }
    protected override void Update()
    {
        base.Update();
        remainTime -= Time.deltaTime;
        remainchargingTime -= Time.deltaTime;
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

        rb2D.velocity = Vector3.zero;
        
        OnCharging(controller.Direction);

        if (remainchargingTime > 0) return;

        OnRush();
    }

    private void OnCharging(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        rushRange.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void OnRush()
    {
        if (!isRush)
        {
            isRush = true;
            float randX = Random.Range(-0.5f, 0.5f);
            float randy = Random.Range(-0.5f, 0.5f);
            targetPos = (Vector2)controller.Target.transform.position + new Vector2 (randX, randy);
            rushRange.SetActive(false);
            // �ӵ��� ��ǥ��ǥ�� ����
        }

        float distance = Vector2.Distance(targetPos, transform.position);
        Vector2 direction = (targetPos- (Vector2)transform.position).normalized;
        rb2D.velocity = direction * speed;
        animationController.Move(direction);

        if (distance < 0.5f)
        {
            remainTime = coolTime;
            targetPos = Vector2.zero;
            isReady = false;
            isRush = false;
        }

    }

    public override bool CheckBehaviour()
    {
        // ��Ÿ���̶�� �ļ����� �о��ش�.
        if (remainTime > 0f) return false;

        if (!isReady)
        {
            // ��Ÿ�Ӹ� �����ϸ� ����
            remainchargingTime = chargingTime;
            isReady = true;
            rushRange.SetActive(true);
        }
        
        return true;

    }
}
