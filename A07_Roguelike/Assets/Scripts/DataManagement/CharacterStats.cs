using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
    Add,
    Multiple,
    Override,
}

[Serializable]
public class CharacterStats
{
    public StatsChangeType statsChangeType;
    [Range(0, 100)] public int maxHealth;
    [Range(0f, 20f)] public float speed;
    public AttackSO attackSO;
}
