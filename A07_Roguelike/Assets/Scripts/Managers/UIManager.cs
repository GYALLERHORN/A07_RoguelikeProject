using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static UIManager Instance;

    private List<UIBase> UIOpened = new List<UIBase>();

    private ObjectPool _pools;
}
