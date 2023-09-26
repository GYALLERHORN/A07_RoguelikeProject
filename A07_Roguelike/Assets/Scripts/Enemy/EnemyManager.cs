using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Enemy들의 프리펩을 가지고 생성 및 관리.
    // 오브젝트 풀을 사용해서 생성할꺼임

    public static EnemyManager Instance { get; private set; }

    [SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SpawnEnemy()
    {
        
    }

    


}
