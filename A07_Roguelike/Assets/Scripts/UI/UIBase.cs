using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [Header("�ֻ��� RectTransform�� ������ ��.")]
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
    /// �� �޼ҵ带 ������� �� ��. SelfCloseUI()�� ����� ��.
    /// </summary>
    public virtual void CloseUI()
    {
        ActAtClose?.Invoke();
        Destroy(gameObject);
    }
    /// <summary>
    /// �� �޼ҵ带 ������� �� ��. SelfHideUI()�� ����� ��.
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
