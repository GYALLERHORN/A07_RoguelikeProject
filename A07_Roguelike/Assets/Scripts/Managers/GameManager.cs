using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public DataManager Data;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Data = DataManager.Get();
    }

    public void StartDunegeon(int level)
    {

    }

    public void StartSpawnMonster(eDungeonType  type)
    {

    }
}

public enum eDungeonType
{
    First,
    Second,
}
