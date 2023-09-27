using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [SerializeField] protected RectTransform _rectTransform;
    protected Action ActAtHide;
    protected Action ActAtClose;
    protected Action ActAtShow;

    public void SetActAtHide(Action action) { ActAtHide = action; }
    public void SetActAtClose(Action action) { ActAtClose = action; }
    public void SetActAtShow(Action action) { ActAtShow = action; }
    
    public virtual void Refresh()
    {

    }

    public virtual void CloseUI()
    {
        ActAtClose?.Invoke();
        Destroy(gameObject);
    }

    public virtual void HideUI()
    {
        ActAtHide?.Invoke();
        gameObject.SetActive(false);
    }

    public virtual void ShowUI()
    {
        ActAtShow?.Invoke();
        gameObject.SetActive(true);
        Refresh();
    }
}
