using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : UIBase
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _data;
    private Action _callback;
    private bool IsTemp;
    private float _time;
    private float _duration;

    private void LateUpdate()
    {
        _time += Time.deltaTime;
        if (IsTemp)
        {
            if (_duration < _time)
                SelfHideUI();
            else if (_duration * 3 < _time)
                SelfCloseUI();
        }

    }

    public void Initialize(string data, string title = null, Action callback = null, bool temp = false, float duration = 0.0f)
    {
        _callback = callback;
        if (title == null)
        {
            _title.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            _title.transform.parent.gameObject.SetActive(true);
            _title.text = title;
        }
        _data.text = data;
        IsTemp = temp;
        _time = 0.0f;
        _duration = duration;
    }

    public override void CloseUI()
    {
        _callback?.Invoke();
        base.CloseUI();
    }

    public override void HideUI()
    {
        _callback?.Invoke();
        base.HideUI();
    }

    private void SelfCloseUI()
    {
        UIManager.CloseUI<UIPopup>();
    }

    private void SelfHideUI()
    {
        UIManager.HideUI<UIPopup>();
    }
}
