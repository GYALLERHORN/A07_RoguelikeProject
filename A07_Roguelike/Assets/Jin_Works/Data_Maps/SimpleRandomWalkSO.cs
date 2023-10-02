using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/SompleRandomWalkData", order = 0)]
public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;

    // iternations : walkLength에 따라 맵 만들기를 반복할 횟수
    // walkLength : 맵(타일)이 뻗어나갈 길이
    // startRandomEachIteration : iternation에 따라 맵 만들기를 반복할 때, 맵(타일) 작성 시작 위치를 0,0이 아닌 다른 곳에서 시작할 것인가?
}
