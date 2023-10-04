using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; // UnityEditor 네임스페이스
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungeonGenerator generator;

    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target; // generator는 AbstractDungeonGenerator를 가리킨다.
        // generator = new SimpleWalkDungeonGenerator(); // 왜 이렇게는 안 쓴거지?
        // generator = new AbstractDungeonGenerator(); // 추상 형식 클래스/인터페이스는 인스턴스할 수 없다.
    }

    public override void OnInspectorGUI() // Graphical User Interface : 기능을 그래픽(아이콘 등)으로 나타낸 것
    {                                      // InspectorGUI : Inspector창에 있는 GUI
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }

}
#endif