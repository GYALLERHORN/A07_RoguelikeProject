using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [Header("최상위 RectTransform을 지정할 것.")]
    [SerializeField] protected RectTransform _self;
    protected Action ActAtHide;
    protected Action ActAtClose;

    public virtual void AddActAtHide(Action action) { ActAtHide += action; }
    public virtual void AddActAtClose(Action action) { ActAtClose += action; }

    protected virtual void OnDisable()
    {
        Invoke("SelfCloseUI", 10f);
    }

    protected virtual void OnEnable()
    {
        CancelInvoke();
    }

    public virtual void Refresh()
    {

    }

    /// <summary>
    /// 이 메소드를 사용하지 말 것. SelfCloseUI()를 사용할 것.
    /// </summary>
    public virtual void CloseUI()
    {
        ActAtClose?.Invoke();
        Destroy(gameObject);
    }
    /// <summary>
    /// 이 메소드를 사용하지 말 것. SelfHideUI()를 사용할 것.
    /// </summary>
    public virtual void HideUI()
    {
        ActAtHide?.Invoke();
        gameObject.SetActive(false);
    }

    protected virtual void SelfCloseUI()
    {
        UIManager.CloseUI(this);
    }

    protected virtual void SelfHideUI()
    {
        UIManager.HideUI(this);
    }
}
