using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    [HideInInspector] public Canvas UICanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (UICanvas == null)
        {
            UICanvas = GameObject.Find("UI")?.GetComponent<Canvas>();
        }
    }

    public void StartDunegeon()
    {

    }

    public void StartSpawnMonster()
    {

    }
}