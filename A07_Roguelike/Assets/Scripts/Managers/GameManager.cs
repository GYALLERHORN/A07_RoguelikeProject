using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    [HideInInspector] public Canvas UICanvas;

    [Header("플래이어")]
    public GameObject PlayerPrefab;
    public GameObject PlayerInActive;

    [Header("던전 맵")]
    [SerializeField] private GameObject Map1;
    [SerializeField] private GameObject Map2;
    [SerializeField] private GameObject MapBoss;

    private int _dungeonFloor = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UICanvas = GameObject.Find("UI")?.GetComponent<Canvas>();
        }
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void LateUpdate()
    {

    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name == "TownScene" || next.name == "DungeonScene")
        {
            UICanvas = GameObject.Find("UI")?.GetComponent<Canvas>();
            PlayerInActive = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        }
        if (next.name == "DungeonScene")
        {
            var dungeon = GameObject.Find("Dungeon");
            switch (_dungeonFloor)
            {
                case 0:
                    Instantiate(Map1, dungeon.transform);
                    break;
                case 1:
                    Instantiate(Map2, dungeon.transform);
                    break;
                case 2:
                    Instantiate(MapBoss, dungeon.transform);
                    break;
                default:
                    break;
            }
        }
    }

    public void StartDungeon()
    {
        EnterDungeon(0);
    }

    public void EnterDungeon(int floor)
    {
        if (floor > 2)
            return;

        _dungeonFloor = floor;
        SceneManager.LoadScene(2);
    }

    public void LeaveDungeon()
    {

    }

    public void EscapeDungeon()
    {

    }

    public void StartSpawnMonster()
    {

    }
}