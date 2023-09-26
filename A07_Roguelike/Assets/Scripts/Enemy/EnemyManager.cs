using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Enemy���� �������� ������ ���� �� ����.
    // ������Ʈ Ǯ�� ����ؼ� �����Ҳ���

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
