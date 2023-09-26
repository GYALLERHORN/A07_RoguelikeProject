using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>(); // HashSet<T> : �ε����� ���� ������ �迭. �ε��� ���� Hash�� �����ȴ�.
                                                              // ���⼭ ����� path�� SimpleWalkDungeonGenerator�� floorPositions, �� Ÿ���� ������ ��ǥ�� ������ �ȴ�.

        path.Add(startPosition); // ù ������ startPosition����. ���� SimpleWalkDungeonGenerator.startRandomlyEachIteration = true�� ��쿡�� �̸����� 0,0�� �ƴ� ������ Ÿ�� ��ǥ �߿��� �������� ��������.
        var previousposition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousposition + Direction2D.GetRandomCardinalDirection(); // newPosition : ������ ��ǥ�� �����¿� ��1 ��ĭ�̵� ���� ���Ѵ�.

            path.Add(newPosition); // ���� ������� ��ǥ�� path�� �߰�
            previousposition = newPosition; // ���� ������� ��ǥ�� �ٽ� ������ ��ǥ�� �ȴ�.
        }

        return path; // path => floorPositions
    }

    public static class Direction2D
    {

        public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>() // cardinal : �⺻����, ���� �߿���
        {
            new Vector2Int(0,1), // UP
            new Vector2Int(0,-1), // DOWN
            new Vector2Int(1,0), // RIGHT
            new Vector2Int(-1,0), // LEFT
        };

        public static Vector2Int GetRandomCardinalDirection()
        {
            return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)]; // cardinalDirectionList����Ʈ�� ���� �ε����� ���� ��
        }
    }
}
