using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyController/Attacks/Range", order = 1)]
public class EnemyRangeAttackSO : ScriptableObject
{
    [Header("Ranged Attack Data")]
    public eAttackType bulletNameTag;
    public float duration;
    public float spread;
    public int numberofProjectilesPerShot;
    public float multipleProjectilesAngle;
    public Color projectileColor;

}
