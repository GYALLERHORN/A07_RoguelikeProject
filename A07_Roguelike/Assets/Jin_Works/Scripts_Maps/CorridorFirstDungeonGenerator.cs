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

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomrPositions); // corridorLength만금 생성된 복도 좌표집합의 집합

        HashSet<Vector2Int> roomPositions = createRooms(potentialRoomrPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions); // 복도의 가장자리 좌표, 가장자리에 방이 없으면 그 복도는 의미가 없다.

        CreateRoomsAtDeadEnds(deadEnds, roomPositions); // 복도 가장자리에 방 만들기

        floorPositions.UnionWith(roomPositions); // 위 메서드로 생성된 방 좌표도 복도 좌표와 함께 바닥 타일을 생성할 좌표다.

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
        List<Vector2Int> newCorridor = new List<Vector2Int>(); // 복도 한 칸당 3x3칸 씩 확장해서 다시 복도 좌표 집합으로 할당한 변수
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

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions) // 복도 좌표 집합을 대상으로 복도의 가장자리 좌표 찾기
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList) // 복도 좌표 집합 요소 하나하나 상하좌우 다 반복해서
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
            }

            if (neighboursCount == 1) // 복도 요소 1개에 인접한 좌표가 1개 밖에 없으면
            {
                deadEnds.Add(position); // 그건 복도의 가장자리
            }
        }
        return deadEnds;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomrPositions)
    {
        var currentPosition = startPosition; // 0,0부터 시작
        potentialRoomrPositions.Add(currentPosition); // 물론, 시작좌표에도 방이 만들어질 수 있다.
        var corridors = new List<List<Vector2Int>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corriderLength); // corridor 좌표 집합 생성 명령
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1]; // 다음 corridor 좌표 생성의 시작점은 현재까지 만들어진 corrider의 마지막 좌표
            potentialRoomrPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor); // 좌표집합 합치기
        }
        return corridors;
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
}