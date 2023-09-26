using System;
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
    private List<Type> UINotClose = new List<Type>();

    private void Awake()
    {
        Instance = this;
        _pools = GetComponent<ObjectPool>();

        //UINotClose.Add();
    }

    public void CloseTopUI(bool isPressed)
    {
        if (isPressed)
        {
            if (UIOpened.Count > 0)
            {
                while (UINotClose.Contains(UIOpened[0].GetType()))
                {
                    if (UIOpened.Count == 2)
                        return;
                    var window = UIOpened[0];
                    UIOpened.RemoveAt(0);
                    UIOpened.Add(window);
                }
                UIOpened[0].CloseUI();
            }
        }
    }

    public T ShowUI<T>(eUIType type, Transform root = null) where T : UIBase
    {
        var obj = _pools.SpawnFromPool((ePoolType)((int)type));
        if (obj != null)
        {
            var uiClass = obj.GetComponent<UIBase>();
            UIOpened.Insert(0, uiClass);

            obj.SetActive(true);
            return uiClass as T;
        }
        else
            return null;
    }

    public void CloseUI<T>() where T : UIBase
    {
        var window = GetUI<T>();
        if (window != null)
        {
            window.CloseUI();
        }
    }

    private T GetUI<T>() where T : UIBase
    {
        foreach (var ui in UIOpened)
        {
            if (ui is T)
                return ui as T;
        }
        return null;
    }

    public void AllCloseUI()
    {
        foreach (var ui in UIOpened)
        {
            ui.CloseUI();
        }
    }

    public bool IsOpened<T>() where T : UIBase
    {
        foreach (var ui in UIOpened)
        {
            if (ui is T)
                return true;
        }
        return false;
    }

    public void RemoveUIInList(UIBase uiBase)
    {
        UIOpened.Remove(uiBase);
    }
}
