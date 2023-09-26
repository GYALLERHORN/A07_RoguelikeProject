using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats baseStats;
    public CharacterStats CurrentStates { get; private set; }
    public List<CharacterStats> statsModifiers = new List<CharacterStats>();

    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }

        CurrentStates = new CharacterStats { attackSO = attackSO };
        // TODO
        CurrentStates.statsChangeType = baseStats.statsChangeType;
        CurrentStates.maxHealth = baseStats.maxHealth;
        CurrentStates.speed = baseStats.speed;

    }
}
