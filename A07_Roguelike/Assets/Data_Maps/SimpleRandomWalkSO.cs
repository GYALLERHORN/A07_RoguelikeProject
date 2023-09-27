using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/SompleRandomWalkData", order = 0)]
public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;

    // iternations : walkLength�� ���� �� ����⸦ �ݺ��� Ƚ��
    // walkLength : ��(Ÿ��)�� ����� ����
    // startRandomEachIteration : iternation�� ���� �� ����⸦ �ݺ��� ��, ��(Ÿ��) �ۼ� ���� ��ġ�� 0,0�� �ƴ� �ٸ� ������ ������ ���ΰ�?
}
