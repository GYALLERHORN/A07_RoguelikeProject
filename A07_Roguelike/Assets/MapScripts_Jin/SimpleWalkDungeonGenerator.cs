using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWalkDungeonGenerator : AbstractDungeonGenerator
{
    //[SerializeField]
    //protected Vector2Int startPosition = Vector2Int.zero; ���� ��ġ 0,0

    [SerializeField]
    private int iterations = 10; // iteration : �ݺ�
    // walkLength�� ���� �� ����⸦ �ݺ��� Ƚ��
    [SerializeField]
    public int walkLength = 10;
    // ��(Ÿ��)�� ����� ����
    [SerializeField]
    public bool startRandomlyEachIteration = true;
    // iternation�� ���� �� ����⸦ �ݺ��� ��, ��(Ÿ��) �ۼ� ���� ��ġ�� 0,0�� �ƴ� �ٸ� ������ ������ ���ΰ�?

    //[SerializeField]
    //private TilemapVisualizer tilemapVisualizer;

    protected override void RunProceduralGeneration() // Generate��ư Ŭ�� �� �̺�Ʈ
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(); // Ÿ�ϸ� ��ǥ ������ �����ϴ� ���
        tilemapVisualizer.Clear(); // ������ �����ƴ� Ÿ�ϸ� ���� ���
        tilemapVisualizer.PaintFloorTile(floorPositions); // floorPositions�� ��ǥ ���տ� ���� Ÿ�ϸ� ���� ���
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); // nȸ�� �ݺ����� ������� ��� Ÿ�ϸ� ��ǥ�� ����

        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path); // UnionWith : HashSet���׸������� ��� ����. A���׸�.UnionWith(B���׸�) => A�� B�� ������ ��ģ��. A�� B�� ���� �����Ѵ�(�� ���׸��� �ߺ��� ��Ҵ� �ϳ��� �����ȴ�). B�� ������� �ʴ´�.

            if (startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
            return floorPositions;
    }
}
