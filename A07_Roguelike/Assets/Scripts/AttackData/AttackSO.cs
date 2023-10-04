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
        sb.AppendLine($"����ũ�� : x{size.ToString("F2")}");
        sb.AppendLine($"�������� : {delay}��");
        sb.AppendLine($"���ݷ� : {power}");
        sb.AppendLine($"���ݼӵ� : {speed}");
        if (isOnKnockback)
        {
            sb.AppendLine($"�˹鼼�� : {knockbackPower}");
            sb.AppendLine($"�˹�ð� : {knockbackTime}");
        }
        return sb.ToString();
    }

}
