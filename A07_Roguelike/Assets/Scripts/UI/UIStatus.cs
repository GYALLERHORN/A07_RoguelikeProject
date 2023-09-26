using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UIStatus : UIBase
{
    private CharacterStatsHandler _character;
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

    public void Initialize(CharacterStatsHandler stats, Action callback = null, bool temp = false, float duration = 0.0f)
    {
        _character = stats;
        _callback = callback;
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
