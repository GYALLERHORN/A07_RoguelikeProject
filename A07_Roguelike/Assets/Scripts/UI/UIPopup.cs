using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : UIBase
{
    [Header("내용이 표시되는 오브젝트")]
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _data;
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
        }

    }

    public void Initialize(string data, string title = null, Action actAtHide = null, bool temp = false, float duration = 0.0f)
    {
        ActAtHide = actAtHide;
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
        transform.localPosition = Vector3.zero;
    }

    public void Move(Vector3 screen)
    {
        transform.localPosition = screen;
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }

    public override void HideUI()
    {
        base.HideUI();
    }

    protected override void SelfCloseUI()
    {
        UIManager.CloseUI(this);
    }

    protected override void SelfHideUI()
    {
        UIManager.HideUI(this);
    }
}
