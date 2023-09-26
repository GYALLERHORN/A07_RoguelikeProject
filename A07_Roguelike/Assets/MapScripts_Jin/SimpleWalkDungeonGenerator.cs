using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWalkDungeonGenerator : AbstractDungeonGenerator
{
    //[SerializeField]
    //protected Vector2Int startPosition = Vector2Int.zero; 시작 위치 0,0

    [SerializeField]
    private int iterations = 10; // iteration : 반복
    // walkLength에 따라 맵 만들기를 반복할 횟수
    [SerializeField]
    public int walkLength = 10;
    // 맵(타일)이 뻗어나갈 길이
    [SerializeField]
    public bool startRandomlyEachIteration = true;
    // iternation에 따라 맵 만들기를 반복할 때, 맵(타일) 작성 시작 위치를 0,0이 아닌 다른 곳에서 시작할 것인가?

    //[SerializeField]
    //private TilemapVisualizer tilemapVisualizer;

    protected override void RunProceduralGeneration() // Generate버튼 클릭 시 이벤트
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(); // 타일맵 좌표 집합을 생성하는 명령
        tilemapVisualizer.Clear(); // 기존에 생성됐던 타일맵 삭제 명령
        tilemapVisualizer.PaintFloorTile(floorPositions); // floorPositions의 좌표 집합에 따라 타일맵 생성 명령
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); // n회차 반복으로 만들어진 모든 타일맵 좌표의 집합

        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path); // UnionWith : HashSet제네릭에서만 사용 가능. A제네릭.UnionWith(B제네릭) => A에 B의 내용을 합친다. A는 B의 값을 포함한다(두 제네릭의 중복된 요소는 하나로 생략된다). B는 변경되지 않는다.

            if (startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
            return floorPositions;
    }
}
