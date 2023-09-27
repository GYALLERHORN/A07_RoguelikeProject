using UnityEngine;


[CreateAssetMenu(fileName = "DefaultAttackData", menuName = "EnemyController/Attacks/Default", order = 0)]
public class EnemySO : RangedAttackData
{
    [Header("Enemy Info")]

    public float followRange;
    public float attackRange;

}
