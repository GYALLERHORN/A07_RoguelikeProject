using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackData", menuName = "TopDownController/Attacks/Ranged", order = 1)]
public class RangedAttackData : AttackSO
{
    [Header("Ranged Attack Data")]
    public eAttackType bulletNameTag;
    public float duration;
    public float spread;
    public int numberofProjectilesPerShot;
    public float multipleProjectilesAngle;
    public Color projectileColor;

    public override string GetInfoString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(base.GetInfoString());
        sb.AppendLine($"����ü : {bulletNameTag}");
        sb.AppendLine($"���ӽð� : {duration}��");
        sb.AppendLine($"�� �� �߻�ü ���� : {numberofProjectilesPerShot}��");
        float angle = spread + 0.5f * numberofProjectilesPerShot * multipleProjectilesAngle;
        sb.AppendLine($"���� : -{angle}��  ~  +{angle}��");
        return sb.ToString();
    }
}
