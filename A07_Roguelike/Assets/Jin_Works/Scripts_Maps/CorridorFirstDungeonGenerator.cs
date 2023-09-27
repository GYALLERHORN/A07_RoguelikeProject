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
    public SimpleRandomWalkSO roomGenerationParameters; // SimpleRandomWalkSO의 방 생성 변수들

    protected override void RunProceduralGeneration()
    {
        // base.RunProceduralGeneration(); => RunProcedualGeneration메서드를 그냥 그대로 쓰겠다는 말. 물론 이 클래스에서는 그대로 안쓴다.
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration() // 복도 좌표 생성 최초메서드
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); // 생성된 모든 좌표의 집합
        HashSet<Vector2Int> potentialRoomrPositions = new HashSet<Vector2Int>(); // 좌표 중 방이 생성될 가능성이 있는 좌표의 집합

        CreateCorridors(floorPositions, potentialRoomrPositions);

        HashSet<Vector2Int> roomPositions = createRooms(potentialRoomrPositions);

        floorPositions.UnionWith(roomPositions); // 위 메서드로 생성된 방 좌표도 복도 좌표와 함께 바닥 타일을 생성할 좌표다.

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
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition); // 각 방 좌표마다 방 생성 변수에 따라 방 좌표 집합 생성
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomrPositions)
    {
        var currentPosition = startPosition; // 0,0부터 시작
        potentialRoomrPositions.Add(currentPosition); // 물론, 시작좌표에도 방이 만들어질 수 있다.

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corriderLength); // corridor 좌표 집합 생성 명령
            floorPositions.UnionWith(corridor); // 좌표집합 합치기

            currentPosition = corridor[corridor.Count - 1]; // 다음 corridor 좌표 생성의 시작점은 현재까지 만들어진 corrider의 마지막 좌표
            potentialRoomrPositions.Add(currentPosition);
        }
    }
}