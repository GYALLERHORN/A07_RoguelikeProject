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
    

    [SerializeField][Range(0f, 100f)] float range;

    [SerializeField][Range(0f, 100f)] float coolTime;
    float remainTime;

    [SerializeField][Range(0f, 100f)] float chargingTime;
    [SerializeField][Range(0f, 100f)] float remainchargingTime;
    [SerializeField][Range(0f, 100f)] float speed;

    Vector2 targetPos = Vector2.zero;
    Vector2 direction = Vector2.zero;
    Vector2 startPos = Vector2.zero;

    private float targetMoveDistance;
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

    // ó������ Range�� ���ߵǴµ� ���߿��� Range�� �ȵ�����
    public override void OnBehaviour()
    {
        if (CheckBehaviour())
        {
            if (!isReady) Init();

            if (remainchargingTime > 0)
            {

                OnCharging();
            }
            else
            {
                OnRush();
            }
        }

        controller.enemyBehaviours.Dequeue();
        controller.enemyBehaviours.Enqueue(this);

    }

    private void Init()
    {
        isReady = true;
        remainchargingTime = chargingTime;
        startPos = transform.position;
        rushRange.SetActive(true);
    }
    private void OnCharging()
    {
        // Ÿ���� Ȯ���� Ÿ�̹��� ������ �˱����ؼ�
        targetPos = (Vector2)controller.Target.transform.position * 1.3f;
        direction = (targetPos - (Vector2)transform.position).normalized;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        rushRange.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
    private void OnRush()
    {
        if (!isRush)
        {
            isRush = true;
            targetMoveDistance = Vector2.Distance(startPos, targetPos);
            rushRange.SetActive(false);
        }

        rb2D.velocity = direction * speed;
        animationController.Move(direction);

        // �̵��� �Ÿ��� ������������ ��ǥ���������� �Ÿ��� ��
        if (Vector2.Distance(startPos, (Vector2)transform.position) > targetMoveDistance)
        {
            rb2D.velocity = Vector2.zero;
            remainTime = coolTime;
            targetPos = Vector2.zero;
            isReady = false;
            isRush = false;
        }

    }

    public override bool CheckBehaviour()
    {
        // ��Ÿ���̶�� �ļ����� �о��ش�.
        if (remainTime > 0f || (controller.Distance > range && !isReady)) return false;
        
        return true;

    }
}
