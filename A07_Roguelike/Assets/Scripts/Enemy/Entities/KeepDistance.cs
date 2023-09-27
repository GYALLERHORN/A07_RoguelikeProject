using UnityEngine;

public class KeepDistance : EnemyBehaviour
{
    // 타겟이 Range 범위 안에 들어온다면 

    // targetDistnae 만큼 거리를 벌린다.

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

    // 발동 조건을 체크해서 시작하고
    // 끝나는 조건을 체크해서 끝낸다.

    // 조건을 만족 시키지 못하면 후순위로 밀어준다.
    public override void OnBehaviour()
    {

        if (!CheckBehaviour()) 
        {
            controller.enemyBehaviours.Dequeue();
            controller.enemyBehaviours.Enqueue(this);
            return;
        }
        
        // Todo 방향을 꼭 플레이어 반대가 아니라 좀 다양하게
        Vector2 direction = Quaternion.Euler(0,0,Random.Range(-10f,10f)) * -controller.Direction * enemySO.speed;
        rb2D.velocity = direction;
        animationController.Move(direction);

        
    }

    public override bool CheckBehaviour()
    {
        // 쿨타임이라면 후순위로 밀어준다.
        if (remainTime > 0f) return false;

        float distance = controller.Distance;

        if (IsReady)
        {
            // 종료조건
            // 목표 거리까지 거리를 벌리고 원래속도로 돌아옴
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
            // 시작조건
            // 플레이어가 일정범위까지 가까이 다가오면
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
