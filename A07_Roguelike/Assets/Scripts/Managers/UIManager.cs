using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum eUIType
{
    //Inventory,
    Status,
    //Store,
    Popup,
    //InventoryItem,
    //OX,
    //Gold,
}

public class UIManager
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new UIManager();
            return _instance;
        }
    }

    private Dictionary<Type, GameObject> _prefabs;
    private LinkedList<UIBase> OpenList;
    private LinkedList<UIBase> HideList;

    public UIManager()
    {
        _prefabs = new Dictionary<Type, GameObject>();
        OpenList = new LinkedList<UIBase>();
        HideList = new LinkedList<UIBase>();
        LoadUIPrefabs();
    }

    public static void HideTopUI()
    {
        if (Instance.OpenList.Count > 0)
        {
            HideUI(Instance.OpenList.First.Value);
        }
    }
    public static UIBase ShowUI(eUIType type, RectTransform root = null)
    {
        switch (type)
        {
            case eUIType.Popup:
                return ShowUI<UIPopup>(root);
            case eUIType.Status:
                return ShowUI<UIInfo>(root);
            default:
                return null;
        }
    }
    private static T ShowUI<T>(RectTransform root = null) where T : UIBase
    {
        var open = GetHideUI<T>();
        if (open != null)
        {
            Instance.HideList.Remove(open);
            if (root == null)
                open.ShowUI(GameManager.Instance.UICanvas.transform);
            else
                open.ShowUI(root);
            return open;
        }

        var prefab = Instance._prefabs[typeof(T)];
        if (prefab != null)
        {
            GameObject obj;
            if (root == null)
                obj = GameObject.Instantiate(prefab, GameManager.Instance.UICanvas.transform);
            else
                obj = GameObject.Instantiate(prefab, root);
            var uiClass = obj.GetComponent<UIBase>();

            Instance.OpenList.AddFirst(uiClass);

            obj.SetActive(true);
            return uiClass as T;
        }
        else
            return null;
    }

    public static void CloseUI<T>(T target) where T : UIBase
    {
        if (IsHide(target))
        {
            target.CloseUI();
            Instance.OpenList.Remove(target);
            Instance.HideList.Remove(target);
        }
        else
        {
            target.CloseUI();
            Instance.OpenList.Remove(target);
        }
    }

    public static void HideUI<T>(T target) where T : UIBase
    {
        if (!IsHide(target))
        {
            target.HideUI();
            Instance.HideList.AddLast(target);
            Instance.OpenList.Remove(target);
            Instance.OpenList.AddLast(target);
        }
    }

    public static T GetOpenUI<T>(T search) where T : UIBase
    {
        foreach (var ui in Instance.OpenList)
        {
            if (ui == search)
                return ui as T;
        }
        return null;
    }

    public static T GetOpenUI<T>() where T : UIBase
    {
        LinkedListNode<UIBase> ui = Instance.OpenList.First;
        while (ui != null)
        {
            if (ui.Value is T)
                return ui.Value as T;
            ui = ui.Next;
        }
        return null;
    }

    public static T GetHideUI<T>(T search) where T : UIBase
    {
        foreach (var ui in Instance.HideList)
        {
            if (ui == search)
                return ui as T;
        }
        return null;
    }

    public static T GetHideUI<T>() where T : UIBase
    {
        LinkedListNode<UIBase> ui = Instance.HideList.First;
        while (ui != null)
        {
            if (ui.Value is T)
                return ui.Value as T;
            ui = ui.Next;
        }
        return null;
    }

    public static void CloseAllOpenUI()
    {
        foreach (var ui in Instance.OpenList)
        {
            ui.CloseUI();
        }
        Instance.OpenList.Clear();
        Instance.HideList.Clear();
    }

    public static void CloseAllHideUI()
    {
        foreach (var ui in Instance.HideList)
        {
            ui.CloseUI();
            Instance.OpenList.Remove(ui);
        }
        Instance.HideList.Clear();
    }

    public static bool IsOpen<T>() where T : UIBase
    {
        foreach (var ui in Instance.OpenList)
        {
            if (ui is T)
                return true;
        }
        return false;
    }

    public static bool IsOpen<T>(T target) where T : UIBase
    {
        foreach (var ui in Instance.OpenList)
        {
            if (ui == target)
                return true;
        }
        return false;
    }

    public static bool IsHide<T>() where T : UIBase
    {
        foreach (var ui in Instance.HideList)
        {
            if (ui is T)
                return true;
        }
        return false;
    }

    public static bool IsHide<T>(T target) where T : UIBase
    {
        foreach (var ui in Instance.HideList)
        {
            if (ui == target)
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
