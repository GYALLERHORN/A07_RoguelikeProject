using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    [HideInInspector] public Canvas UICanvas;

    [Header("´øÀü ¸Ê")]
    [SerializeField] private GameObject Map1;
    [SerializeField] private GameObject Map2;
    [SerializeField] private GameObject MapBoss;

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

    public void StartDungeon()
    {
        EnterDungeon(0);
    }

    public void EnterDungeon(int floor)
    {
        switch (floor)
        {
            case 0:
                
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                ExitDungeon();
                break;
        }
    }

    public void ExitDungeon()
    {

    }

    public void StartSpawnMonster()
    {

    }
}