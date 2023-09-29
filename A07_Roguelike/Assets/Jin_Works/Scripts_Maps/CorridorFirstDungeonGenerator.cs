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
    public SimpleRandomWalkSO roomGenerationParameters; // SimpleRandomWalkSO�� �� ���� ������

    protected override void RunProceduralGeneration()
    {
        // base.RunProceduralGeneration(); => RunProcedualGeneration�޼��带 �׳� �״�� ���ڴٴ� ��. ���� �� Ŭ���������� �״�� �Ⱦ���.
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration() // ���� ��ǥ ���� ���ʸ޼���
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); // ������ ��� ��ǥ�� ����
        HashSet<Vector2Int> potentialRoomrPositions = new HashSet<Vector2Int>(); // ��ǥ �� ���� ������ ���ɼ��� �ִ� ��ǥ�� ����

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomrPositions); // corridorLength���� ������ ���� ��ǥ������ ����

        HashSet<Vector2Int> roomPositions = createRooms(potentialRoomrPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions); // ������ �����ڸ� ��ǥ, �����ڸ��� ���� ������ �� ������ �ǹ̰� ����.

        CreateRoomsAtDeadEnds(deadEnds, roomPositions); // ���� �����ڸ��� �� �����

        floorPositions.UnionWith(roomPositions); // �� �޼���� ������ �� ��ǥ�� ���� ��ǥ�� �Բ� �ٴ� Ÿ���� ������ ��ǥ��.

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorSizeSyzeByOne(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

        tilemapVisualizer.PaintFloorTile(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private List<Vector2Int> IncreaseCorridorSizeSyzeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>(); // ���� �� ĭ�� 3x3ĭ �� Ȯ���ؼ� �ٽ� ���� ��ǥ �������� �Ҵ��� ����
        //var previousDirection = Vector2Int.zero;
        for (int i = 1; i < corridor.Count; i++)
        {
            //Vector2Int directionFromCell = corridor[i] - corridor[i - 1];
            //if (previousDirection != Vector2Int.zero && directionFromCell != previousDirection)
            //{
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i-1] + new Vector2Int(x, y));
                    }
                }
                //previousDirection = directionFromCell;
            //}
            //else
            //{
            //    Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
            //    newCorridor.Add(corridor[i - 1]);
            //    newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            //}
        }
        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
            return Vector2Int.right;
        if (direction == Vector2Int.right)
            return Vector2Int.down;
        if (direction == Vector2Int.down)
            return Vector2Int.left;
        if (direction == Vector2Int.left)
            return Vector2Int.up;
        return Vector2Int.zero;
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (!roomFloors.Contains(position))
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions) // ���� ��ǥ ������ ������� ������ �����ڸ� ��ǥ ã��
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList) // ���� ��ǥ ���� ��� �ϳ��ϳ� �����¿� �� �ݺ��ؼ�
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
            }

            if (neighboursCount == 1) // ���� ��� 1���� ������ ��ǥ�� 1�� �ۿ� ������
            {
                deadEnds.Add(position); // �װ� ������ �����ڸ�
            }
        }
        return deadEnds;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomrPositions)
    {
        var currentPosition = startPosition; // 0,0���� ����
        potentialRoomrPositions.Add(currentPosition); // ����, ������ǥ���� ���� ������� �� �ִ�.
        var corridors = new List<List<Vector2Int>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corriderLength); // corridor ��ǥ ���� ���� ���
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1]; // ���� corridor ��ǥ ������ �������� ������� ������� corrider�� ������ ��ǥ
            potentialRoomrPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor); // ��ǥ���� ��ġ��
        }
        return corridors;
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
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition); // �� �� ��ǥ���� �� ���� ������ ���� �� ��ǥ ���� ����
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }
}