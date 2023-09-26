using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [SerializeField] protected RectTransform _rectTransform;
    [SerializeField] protected Canvas _canvas;

    protected bool _isTemp;

    public virtual void Refresh()
    {

    }

    public virtual void CloseUI()
    {
        UIManager.Instance.RemoveUIInList(this);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
