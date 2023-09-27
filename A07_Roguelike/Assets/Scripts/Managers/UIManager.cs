using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public static UIManager Instance;

    private Dictionary<Type, GameObject> _prefabs = new Dictionary<Type, GameObject>();
    private List<UIBase> OpenedUI = new List<UIBase>();
    private List<Type> DoNotCloseUI = new List<Type>();

    public UIManager()
    {
        LoadUIPrefabs();
    }

    public void CloseTopUI(bool isPressed)
    {
        if (isPressed)
        {
            if (OpenedUI.Count > 0)
            {
                while (DoNotCloseUI.Contains(OpenedUI[0].GetType()))
                {
                    if (OpenedUI.Count == 2)
                        return;
                    var window = OpenedUI[0];
                    OpenedUI.RemoveAt(0);
                    OpenedUI.Add(window);
                }
                OpenedUI[0].CloseUI();
            }
        }
    }

    public T ShowUI<T>(RectTransform root = null) where T : UIBase
    {
        if (_prefabs.Count <= 0)
        {
            LoadUIPrefabs();
            return null;
        }

        var prefab = _prefabs[typeof(T)];
        if (prefab != null)
        {
            GameObject obj;
            if (root == null)
                obj = GameObject.Instantiate(prefab);
            else
                obj = GameObject.Instantiate(prefab, root);
            var uiClass = obj.GetComponent<UIBase>();
            OpenedUI.Insert(0, uiClass);

            obj.SetActive(true);
            return uiClass as T;
        }
        else
            return null;
    }

    public void CloseUI<T>() where T : UIBase
    {
        var window = GetOpenedUI<T>();
        if (window != null)
        {
            window.CloseUI();
        }
    }

    private T GetOpenedUI<T>() where T : UIBase
    {
        foreach (var ui in OpenedUI)
        {
            if (ui is T)
                return ui as T;
        }
        return null;
    }

    public void CloaseAllOpenedUI()
    {
        foreach (var ui in OpenedUI)
        {
            ui.CloseUI();
        }
    }

    public bool IsOpened<T>() where T : UIBase
    {
        foreach (var ui in OpenedUI)
        {
            if (ui is T)
                return true;
        }
        return false;
    }

    public void LoadUIPrefabs()
    {
        var objs = Resources.LoadAll<GameObject>("Prefabs/UI/");
        foreach (var obj in objs)
        {
            var type = obj.GetComponent<UIBase>().GetType();
            _prefabs.Add(type, obj);
            Debug.Log($"{type}(Prefabs/UI/{obj.name}) is loaded.");
        }
    }
}
