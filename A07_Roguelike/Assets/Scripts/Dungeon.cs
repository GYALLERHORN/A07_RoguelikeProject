using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    [SerializeField] private GameObject startPosObj;
    [SerializeField] private GameObject endPosObj;
    [SerializeField] private GameObject monsterSpawnPointsObj;

    public Vector2 StartPos { get; private set; }
    public Vector2 EndPos { get; private set; }
    public List<Vector2> SpwanPosList { get; private set; }

    private void Awake()
    {
        SpwanPosList = new List<Vector2>();

        StartPos = startPosObj.transform.position;
        EndPos = endPosObj.transform.position;
        
        int count = monsterSpawnPointsObj.transform.childCount;

        for (int i =0; i< count; i++)
        {
            SpwanPosList.Add(monsterSpawnPointsObj.transform.GetChild(i).transform.position);
        }
    }
}
