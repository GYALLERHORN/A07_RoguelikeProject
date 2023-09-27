using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private SimpleRandomWalkSO randomWalkParameters;

    protected override void RunProceduralGeneration() // Generate��ư Ŭ�� �� �̺�Ʈ
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(); // Ÿ�ϸ� ��ǥ ������ �����ϴ� ���
        tilemapVisualizer.Clear(); // ������ �����ƴ� Ÿ�ϸ� ���� ���
        tilemapVisualizer.PaintFloorTile(floorPositions); // floorPositions�� ��ǥ ���տ� ���� Ÿ�ϸ� ���� ���
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); // nȸ�� �ݺ����� ������� ��� Ÿ�ϸ� ��ǥ�� ����

        for (int i = 0; i < randomWalkParameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);
            floorPositions.UnionWith(path); // UnionWith : HashSet���׸������� ��� ����. A���׸�.UnionWith(B���׸�) => A�� B�� ������ ��ģ��. A�� B�� ���� �����Ѵ�(�� ���׸��� �ߺ��� ��Ҵ� �ϳ��� �����ȴ�). B�� ������� �ʴ´�.

            if (randomWalkParameters.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
            return floorPositions;
    }
}
