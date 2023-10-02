using System;
using System.Collections.Generic;
using UnityEngine;

public enum eMonsterName
{
    // 몬스터 프리펩의 이름을 등록
    GreenSlime,
    BlueSlime,
    RedSlime,
}

public class EnemyManager 
{
    private static EnemyManager _instance;

    public static EnemyManager Instance
    {
        get
        {
            if(_instance == null )
                _instance = new EnemyManager();
            return _instance;
        }
    }

    public EnemyManager()
    {
        Prefabs = new Dictionary<eMonsterName, GameObject>();
        LoadEnemyPrefabs();
    }

    public Dictionary<eMonsterName, GameObject> Prefabs { get; private set; }

    private void LoadEnemyPrefabs()
    {
        var gameObjects = Resources.LoadAll<GameObject>("Prefabs/Enemy");

        foreach (GameObject go in gameObjects)
        {
            object key;
            if (Enum.TryParse(typeof(eMonsterName), go.name, out key))
            {
                Prefabs.Add((eMonsterName)key, go);
            }
            else
            {
                Debug.Log("eMonsterName에 없는 몬스터 입니다.");
            }
            
        }
    }

    // 지정한 몬스터 한마리를 지정 위치에 소환
    public void SpawnMonster(eMonsterName type, Vector2 spawnPoint)
    {
        GameObject prefab = Prefabs[type];
        GameObject.Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
   
    public void SpawnMonster(eMonsterName type, Vector2 spawnPoint, int spawnNum)
    {
        for(int i =0; i < spawnNum; i++)
        {
            GameObject prefab = Prefabs[type];
            GameObject.Instantiate(prefab, spawnPoint, Quaternion.identity);
        }
    }
    

    // 지정된 몬스터 집합의 몬스터를 spawnPoint에 소환한다
    public void SpawnMonster(eMonsterName[] typeArray, Vector2[] spawnPoints)
    {
        eMonsterName key = (eMonsterName)UnityEngine.Random.Range(0, typeArray.Length);
        int index = UnityEngine.Random.Range(0, spawnPoints.Length);
        Vector2 spawnPoint = spawnPoints[index];
        GameObject prefab = Prefabs[key];
        GameObject.Instantiate(prefab, spawnPoint, Quaternion.identity);
    }

    public void SpawnMonster(eMonsterName[] typeArray, Vector2[] spawnPoints, int spawnNum)
    {
        for(int i =0; i< spawnNum; i++)
        {
            eMonsterName key = (eMonsterName)UnityEngine.Random.Range(0, typeArray.Length);
            int index = UnityEngine.Random.Range(0, spawnPoints.Length);
            Vector2 spawnPoint = spawnPoints[index];
            GameObject prefab = Prefabs[key];
            GameObject.Instantiate(prefab, spawnPoint, Quaternion.identity);
        }
    }

}
