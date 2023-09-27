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

    protected override void RunProceduralGeneration() // Generate버튼 클릭 시 이벤트. 사실 상 복도 생성이 완료된 시점에서 사용하지 않는다.
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition); // 타일맵 좌표 집합을 생성하는 명령
        tilemapVisualizer.Clear(); // 기존에 생성됐던 타일맵 삭제 명령
        tilemapVisualizer.PaintFloorTile(floorPositions); // floorPositions의 좌표 집합에 따라 타일맵 생성 명령
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }


    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position) // CorridorFirstDungeonGEneration클래스에서 재사용하기 위해 매개변수를 넣었다?
    {                                                  // randomWalkParameters필드가 private이니까 하위 클래스에서 이 필드를 사용하려면 또 필드선언을 해야 한다. 그래서 굳이 매개변수로 넣음
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); // n회차 반복으로 만들어진 모든 타일맵 좌표의 집합

        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkLength);
            floorPositions.UnionWith(path); // UnionWith : HashSet제네릭에서만 사용 가능. A제네릭.UnionWith(B제네릭) => A에 B의 내용을 합친다. A는 B의 값을 포함한다(두 제네릭의 중복된 요소는 하나로 생략된다). B는 변경되지 않는다.

            if (parameters.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count)); // 현재 추가된 좌표 집합 중 무작위로 하나 골라 다음 좌표 생성 시작점으로 설정한다.
            }
        }
            return floorPositions;
    }
}
