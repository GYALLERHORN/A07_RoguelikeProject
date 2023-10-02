using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    public Canvas UICanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartGame()
    {

    }

    public void StartDunegeon()
    {

    }

    public void StartSpawnMonster()
    {

    }
}