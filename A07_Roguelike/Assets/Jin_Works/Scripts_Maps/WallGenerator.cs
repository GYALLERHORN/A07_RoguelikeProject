using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {              // floorPositions : �ٴڿ� �� �� ��ǥ ����
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        foreach (var position in basicWallPositions)
        {
            tilemapVisualizer.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions) // �ٴ� �� ��ǥ����
        {
            foreach (var direction in directionsList) // �����¿� 4���� ���� üũ�ϸ鼭
            {
                var neighbourPosition = position + direction; // �ٴڸ� ��ǥ�� ������ ��ǥ�� - 
                if (!floorPositions.Contains(neighbourPosition)) // �ٴڸ� ��ǥ�� ���ԵǾ� ���� �ʴٸ� => ����ִ� ��ǥ��� 
                {
                    wallPositions.Add(neighbourPosition); // �� ��ǥ�� ���� ���� ��ǥ��.
                }
            }
        }
        return wallPositions;
    }
}
