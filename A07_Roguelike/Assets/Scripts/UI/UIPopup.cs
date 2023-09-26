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
        if (IsTemp)
        {
            _time += Time.deltaTime;
            if (_duration < _time)
                CloseUI();
        }
    }

    public void Initialize(string data, string title = null, Action callback = null, bool temp = false, float duration = 0.0f)
    {
        _callback = callback;
        if (title == null)
        {
            _title.gameObject.SetActive(false);
        }
        else
        {
            _title.gameObject.SetActive(true);
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
}
