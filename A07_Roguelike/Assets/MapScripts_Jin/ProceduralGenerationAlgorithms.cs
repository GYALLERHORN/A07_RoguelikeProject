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

    public static class Direction2D
    {

        public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>() // cardinal : 기본적인, 가장 중요한
        {
            new Vector2Int(0,1), // UP
            new Vector2Int(0,-1), // DOWN
            new Vector2Int(1,0), // RIGHT
            new Vector2Int(-1,0), // LEFT
        };

        public static Vector2Int GetRandomCardinalDirection()
        {
            return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)]; // cardinalDirectionList리스트의 랜덤 인덱스에 따른 값
        }
    }
}
