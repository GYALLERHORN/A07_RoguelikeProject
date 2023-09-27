using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleWalkDungeonGenerator
{
    [SerializeField]
    private int corriderLength = 14, corridorCount = 5; // 생성될 복도 길이 / 복도 생성 횟수
    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercent; // 생성할 방의 비율
    [SerializeField]
    public SimpleRandomWalkSO roomGenerationParameters;


    protected override void RunProceduralGeneration()
    {
        // base.RunProceduralGeneration(); => RunProcedualGeneration메서드를 그냥 그대로 쓰겠다는 말. 물론 이 클래스에서는 그대로 안쓴다.
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
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>(); // 생성될 방의 위치 집합
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
        // 생성될 방의 갯수. 모든 potentialRoomPositions에 생성하지 않고, roomPercent에 따라 적절히 감소시킨다. Mathf.RoundToInt() :정수로 반올림

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        // GUID : 전역 고유 식별자. Guid.NewGuid() : 고유한 키 생성. Take(정수값) : 배열 중 처음 인덱스부터 정수값 갯수까지만 할당한다.
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
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corriderLength); // corridor 좌표 집합 생성 명령
            floorPositions.UnionWith(corridor); // 좌표집합 합치기

            currentPosition = corridor[corridor.Count - 1]; // 다음 corridor 좌표 생성의 시작점은 현재까지 만들어진 corrider의 마지막 좌표
            potentialRoomrPositions.Add(currentPosition);

        }
    }
}
