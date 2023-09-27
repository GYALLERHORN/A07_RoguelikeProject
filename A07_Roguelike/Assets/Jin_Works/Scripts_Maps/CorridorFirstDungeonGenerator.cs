using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleWalkDungeonGenerator
{
    [SerializeField]
    private int corriderLength = 14, corridorCount = 5; // ������ ���� ���� / ���� ���� Ƚ��
    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercent; // ������ ���� ����
    [SerializeField]
    public SimpleRandomWalkSO roomGenerationParameters;


    protected override void RunProceduralGeneration()
    {
        // base.RunProceduralGeneration(); => RunProcedualGeneration�޼��带 �׳� �״�� ���ڴٴ� ��. ���� �� Ŭ���������� �״�� �Ⱦ���.
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomrPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomrPositions);

        HashSet<Vector2Int> roomPositions = createRooms(potentialRoomrPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTile(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private HashSet<Vector2Int> createRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>(); // ������ ���� ��ġ ����
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
        // ������ ���� ����. ��� potentialRoomPositions�� �������� �ʰ�, roomPercent�� ���� ������ ���ҽ�Ų��. Mathf.RoundToInt() :������ �ݿø�

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        // GUID : ���� ���� �ĺ���. Guid.NewGuid() : ������ Ű ����. Take(������) : �迭 �� ó�� �ε������� ������ ���������� �Ҵ��Ѵ�.
        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition); 
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomrPositions)
    {
        var currentPosition = startPosition;
        potentialRoomrPositions.Add(currentPosition);

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corriderLength); // corridor ��ǥ ���� ���� ���
            floorPositions.UnionWith(corridor); // ��ǥ���� ��ġ��

            currentPosition = corridor[corridor.Count - 1]; // ���� corridor ��ǥ ������ �������� ������� ������� corrider�� ������ ��ǥ
            potentialRoomrPositions.Add(currentPosition);

        }
    }
}
