using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // UnityEditor ���ӽ����̽�

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungeonGenerator generator;

    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target; // generator�� AbstractDungeonGenerator�� ����Ų��.
        // generator = new SimpleWalkDungeonGenerator(); // �� �̷��Դ� �� ������?
        // generator = new AbstractDungeonGenerator(); //�߻� ���� Ŭ����/�������̽��� �ν��Ͻ��� �� ����.
    }

    public override void OnInspectorGUI() // Graphical User Interface : ����� �׷���(������ ��)���� ��Ÿ�� ��
    {                                      // InspectorGUI : Inspectorâ�� �ִ� GUI
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }
}
