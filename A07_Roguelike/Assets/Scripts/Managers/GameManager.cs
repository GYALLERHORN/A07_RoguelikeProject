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
    [HideInInspector] public GameObject PlayerInActive;
    private HealthController _healthController;
    private CharacterStatsHandler _characterStatsHandler;
    private InventoryHandler _Inventory;
    private bool isInit = false;

    [Header("던전 맵")]
    [SerializeField] private GameObject Map1;
    [SerializeField] private GameObject Map2;
    [SerializeField] private GameObject MapBoss;
    [SerializeField] private GameObject Entrance;

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

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name == "TownScene")
        {
            UICanvas = GameObject.Find("UI")?.GetComponent<Canvas>();
            PlayerInActive = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            if (isInit)
            {
                TransferData();
            }
            else
            {
                isInit = true;
                _characterStatsHandler = PlayerInActive.GetComponent<CharacterStatsHandler>();
                _Inventory = PlayerInActive.GetComponent<InventoryHandler>();
                _healthController = PlayerInActive.GetComponent<HealthController>();
                _Inventory.InitItem(_characterStatsHandler);
            }
        }
        else if (next.name == "DungeonScene")
        {
            var dungeon = GameObject.Find("Dungeon");
            GameObject obj;
            switch (_dungeonFloor)
            {
                case 0:
                    obj = Instantiate(Map1, dungeon.transform);
                    break;
                case 1:
                    obj = Instantiate(Map2, dungeon.transform);
                    break;
                case 2:
                    obj = Instantiate(MapBoss, dungeon.transform);
                    break;
                default:
                    EscapeDungeon();
                    return;
            }
            var posInfo = obj.GetComponent<Dungeon>();
            StartSpawnMonster(posInfo);

            UICanvas = GameObject.Find("UI")?.GetComponent<Canvas>();
            PlayerInActive = Instantiate(PlayerPrefab, posInfo.StartPos, Quaternion.identity);
            if (isInit)
            {
                TransferData();
            }
            var exit = Instantiate(Entrance, posInfo.EndPos, Quaternion.identity);
            exit.GetComponent<DungeonEntranceHandler>().Initialize(() =>
            {
                EnterDungeon(_dungeonFloor+1);
            });
            
        }
    }

    public void StartDungeon()
    {
        EnterDungeon(0);
    }

    public void EnterDungeon(int floor)
    {
        if (floor > 2)
        {
            EscapeDungeon();
            return;
        }

        _dungeonFloor = floor;
        SceneManager.LoadScene(2);
    }

    private void TransferData()
    {
        PlayerInActive.GetComponent<CharacterStatsHandler>().statsModifiers = _characterStatsHandler.statsModifiers;
        PlayerInActive.GetComponent<HealthController>().LoadHealthController(_healthController);
        PlayerInActive.GetComponent<InventoryHandler>().Initialize(_Inventory);

        _characterStatsHandler = PlayerInActive.GetComponent<CharacterStatsHandler>();
        _characterStatsHandler.UpdateCharacterStats();
        _Inventory = PlayerInActive.GetComponent<InventoryHandler>();
        _healthController = PlayerInActive.GetComponent<HealthController>();
    }

    public void LeaveDungeon()
    {
        SceneManager.LoadScene(1);
    }

    public void EscapeDungeon()
    {
        SceneManager.LoadScene(1);
        isInit = false;
    }

    public void StartSpawnMonster(Dungeon dungeon)
    {
        if (dungeon.SpwanPosList.Count <= 0)
            return;

        eMonsterName[] types = new eMonsterName[Random.Range(1,4)];
        for(int i = 0; i < types.Length; ++i)
        {
            types[i] = (eMonsterName)Random.Range((int)eMonsterName.GreenSlime, (int)eMonsterName.RedSlime + 1);
        }
        EnemyManager.Instance.SpawnMonster(types, dungeon.SpwanPosList.ToArray(), dungeon.SpwanPosList.Count);
    }
}