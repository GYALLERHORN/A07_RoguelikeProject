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
        sb.AppendLine($"투사체 : {bulletNameTag}");
        sb.AppendLine($"지속시간 : {duration}초");
        sb.AppendLine($"샷 당 발사체 개수 : {numberofProjectilesPerShot}개");
        float angle = spread + 0.5f * numberofProjectilesPerShot * multipleProjectilesAngle;
        sb.AppendLine($"각도 : -{angle}°  ~  +{angle}°");
        return sb.ToString();
    }
}
