using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Enemy���� �������� ������ ���� �� ����.

    [SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();

    private void Awake()
    {
        
    }
}
