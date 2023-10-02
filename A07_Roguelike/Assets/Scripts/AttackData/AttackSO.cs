using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[CreateAssetMenu(fileName = "DefaultAttackData", menuName = "TopDownController/Attacks/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;

    [Header("Knock Back Info")]
    public bool isOnKnockback;
    public float knockbackPower;
    public float knockbackTime;

    public virtual string GetInfoString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"공격크기 : x{size.ToString("F2")}");
        sb.AppendLine($"공격지연 : {delay}초");
        sb.AppendLine($"공격력 : {power}");
        sb.AppendLine($"공격속도 : {speed}");
        if (isOnKnockback)
        {
            sb.AppendLine($"넉백세기 : {knockbackPower}");
            sb.AppendLine($"넉백시간 : {knockbackTime}");
        }
        return sb.ToString();
    }

}
