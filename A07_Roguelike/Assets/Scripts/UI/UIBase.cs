using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [SerializeField] protected RectTransform _rectTransform;

    public virtual void Refresh()
    {

    }

    public virtual void CloseUI()
    {
        UIManager.Instance.RemoveUIInList(this);
        gameObject.SetActive(false);
    }
}
