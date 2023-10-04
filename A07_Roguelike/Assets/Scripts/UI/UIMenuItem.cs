using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMenuItem : UIBase
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private AudioClip _clickClip;
    private Action _actAtClick;

    public void Initialize(string title, Action openAtClick)
    {
        _text.text = title;
        _actAtClick = openAtClick;
    }

    public void OnClick()
    {
        _actAtClick?.Invoke();
        if (_clickClip != null )
            SoundManager.PlayClip(eSoundType.UI, _clickClip);
    }
}
