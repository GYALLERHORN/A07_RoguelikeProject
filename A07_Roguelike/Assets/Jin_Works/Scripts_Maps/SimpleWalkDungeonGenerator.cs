using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;

    protected override void RunProceduralGeneration() // Generate��ư Ŭ�� �� �̺�Ʈ. ��� �� ���� ������ �Ϸ�� �������� ������� �ʴ´�.
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition); // Ÿ�ϸ� ��ǥ ������ �����ϴ� ���
        tilemapVisualizer.Clear(); // ������ �����ƴ� Ÿ�ϸ� ���� ���
        tilemapVisualizer.PaintFloorTile(floorPositions); // floorPositions�� ��ǥ ���տ� ���� Ÿ�ϸ� ���� ���
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }


    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position) // CorridorFirstDungeonGEnerationŬ�������� �����ϱ� ���� �Ű������� �־���?
    {                                                  // randomWalkParameters�ʵ尡 private�̴ϱ� ���� Ŭ�������� �� �ʵ带 ����Ϸ��� �� �ʵ弱���� �ؾ� �Ѵ�. �׷��� ���� �Ű������� ����
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); // nȸ�� �ݺ����� ������� ��� Ÿ�ϸ� ��ǥ�� ����

        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkLength);
            floorPositions.UnionWith(path); // UnionWith : HashSet���׸������� ��� ����. A���׸�.UnionWith(B���׸�) => A�� B�� ������ ��ģ��. A�� B�� ���� �����Ѵ�(�� ���׸��� �ߺ��� ��Ҵ� �ϳ��� �����ȴ�). B�� ������� �ʴ´�.

            if (parameters.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count)); // ���� �߰��� ��ǥ ���� �� �������� �ϳ� ��� ���� ��ǥ ���� ���������� �����Ѵ�.
            }
        }
            return floorPositions;
    }
}
