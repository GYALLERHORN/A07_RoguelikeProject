using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager
{
    private static UIManager _instance;
    private static UIManager InstanceCheck
    {
        get {
            if (_instance == null)
                _instance = new UIManager();
            return _instance;
        }
    }

    private Dictionary<Type, GameObject> _prefabs;
    private List<UIBase> OpenedUI;
    private List<UIBase> HidedUI;

    public UIManager()
    {
        _prefabs = new Dictionary<Type, GameObject>();
        OpenedUI = new List<UIBase>();
        HidedUI = new List<UIBase>();
        LoadUIPrefabs();
    }

    public static void CloseTopUI()
    {
        if (InstanceCheck.OpenedUI.Count > 0)
        {
            InstanceCheck.OpenedUI[0].CloseUI();
        }
    }
    public static UIBase ShowUI(eUIType type, RectTransform root = null)
    {
        switch (type)
        {
            case eUIType.Popup:
                return ShowUI<UIPopup>(root);
            case eUIType.Status:
                return ShowUI<UIStatus>(root);
            default:
                return null;
        }
    }
    private static T ShowUI<T>(RectTransform root = null) where T : UIBase
    {
        var open = InstanceCheck.GetOpenedUI<T>();
        if (open != null)
        {
            if (InstanceCheck.GetHidedUI<T>())
            {
                open.gameObject.SetActive(true);
                return open;
            }
            else
                return null;
        }

        var prefab = InstanceCheck._prefabs[typeof(T)];
        if (prefab != null)
        {
            GameObject obj;
            if (root == null)
                obj = GameObject.Instantiate(prefab, GameManager.Instance.UICanvas.transform);
            else
                obj = GameObject.Instantiate(prefab, root);
            var uiClass = obj.GetComponent<UIBase>();

            InstanceCheck.OpenedUI.Insert(0, uiClass);

            obj.SetActive(true);
            return uiClass as T;
        }
        else
            return null;
    }
    public static void CloseUI(eUIType type)
    {
        switch (type)
        {
            case eUIType.Popup:
                CloseUI<UIPopup>();
                break;
            case eUIType.Status:
                CloseUI<UIStatus>();
                break;
            default:
                break;
        }
    }
    private static void CloseUI<T>() where T : UIBase
    {
        var window = InstanceCheck.GetOpenedUI<T>();
        if (window != null)
        {
            window.CloseUI();
            InstanceCheck.OpenedUI.Remove(window);
            InstanceCheck.HidedUI.Remove(window);
            return;
        }
    }
    public static void HideUI(eUIType type)
    {
        switch (type)
        {
            case eUIType.Popup:
                HideUI<UIPopup>();
                break;
            case eUIType.Status:
                HideUI<UIStatus>();
                break;
            default:
                break;
        }
    }
    private static void HideUI<T>() where T : UIBase
    {
        var window = InstanceCheck.GetOpenedUI<T>();
        if (window != null)
        {
            window.HideUI();
            InstanceCheck.HidedUI.Add(window);
        }
    }

    private T GetOpenedUI<T>() where T : UIBase
    {
        foreach (var ui in InstanceCheck.OpenedUI)
        {
            if (ui is T)
                return ui as T;
        }
        return null;
    }

    private T GetHidedUI<T>() where T : UIBase
    {
        foreach (var ui in InstanceCheck.HidedUI)
        {
            if (ui is T)
                return ui as T;
        }
        return null;
    }

    public static void CloseAllOpenedUI()
    {
        foreach (var ui in InstanceCheck.OpenedUI)
        {
            ui.CloseUI();
        }
        InstanceCheck.OpenedUI.Clear();
        InstanceCheck.HidedUI.Clear();
    }

    public static void CloseAllHidedUI()
    {
        foreach (var ui in InstanceCheck.HidedUI)
        {
            ui.CloseUI();
        }
        InstanceCheck.HidedUI.Clear();
    }

    public static bool IsOpened<T>() where T : UIBase
    {
        foreach (var ui in InstanceCheck.OpenedUI)
        {
            if (ui is T)
                return true;
        }
        return false;
    }

    public static bool IsHided<T>() where T : UIBase
    {
        foreach (var ui in InstanceCheck.HidedUI)
        {
            if (ui is T)
                return true;
        }
        return false;
    }

    private void LoadUIPrefabs()
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
