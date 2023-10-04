using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>(); // HashSet<T> : 인덱스가 없는 제레릭 배열. 인덱스 없이 Hash로 관리된다.
                                                              // 여기서 만드는 path가 SimpleWalkDungeonGenerator의 floorPositions, 즉 타일이 생성될 좌표의 집합이 된다.

        path.Add(startPosition); // 첫 시작은 startPosition부터. 물론 SimpleWalkDungeonGenerator.startRandomlyEachIteration = true일 경우에는 이마저도 0,0이 아닌 생성된 타일 좌표 중에서 랜덤으로 정해진다.
        var previousposition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousposition + Direction2D.GetRandomCardinalDirection(); // newPosition : 기존의 좌표에 상하좌우 택1 한칸이동 값을 더한다.

            path.Add(newPosition); // 새로 만들어진 좌표를 path에 추가
            previousposition = newPosition; // 새로 만들어진 좌표는 다시 기존의 좌표가 된다.
        }

        return path; // path => floorPositions
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength) // 실행되면 corridorLength만큼 좌표 집합 한줄이 상하좌우택1 쭉 깔린다.
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight) // BSP 방 생성 알고리즘
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        var roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), 
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z)); ;
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z)); ;
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{

    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>() // cardinal : 기본적인, 가장 중요한
        {
            new Vector2Int(0,1), // UP
            new Vector2Int(1,0), // RIGHT
            new Vector2Int(0,-1), // DOWN
            new Vector2Int(-1,0), // LEFT
        }; 
    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>() // diagonal : 사선의, 대각선의
        {
            new Vector2Int(1,1), // UP-RIGHT
            new Vector2Int(1,-1), // RIGHT-DOWN
            new Vector2Int(-1,-1), // DOWN-LEFT
            new Vector2Int(-1,1), // LEFT-UP
        };
    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>() // 벽 좌표를 중심으로 인접한 8방향의 관계에 따라 벽 타일이 달라진다.
        {
            new Vector2Int(0,1), // UP
            new Vector2Int(1,1), // UP-RIGHT
            new Vector2Int(1,0), // RIGHT
            new Vector2Int(1,-1), // RIGHT-DOWN
            new Vector2Int(0,-1), // DOWN
            new Vector2Int(-1,-1), // DOWN-LEFT
            new Vector2Int(-1,0), // LEFT
            new Vector2Int(-1,1), // LEFT-UP
        };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)]; // cardinalDirectionList리스트의 랜덤 인덱스에 따른 값
    }
}
