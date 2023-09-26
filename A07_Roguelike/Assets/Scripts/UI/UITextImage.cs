using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextImage : UIBase
{
    [SerializeField] private List<Image> _images;
    [SerializeField] private List<TMP_Text> _texts;
    private Action _callback;

    public void Initialize(Sprite[] images, string[] texts, Action callback)
    {
        _callback = callback;
        for (int i = 0; i < _images.Count && i < images.Length; i++)
        {
            _images[i].sprite = images[i];
        }
        for (int i = 0; i < _texts.Count && i < texts.Length; i++)
        {
            _texts[i].text = texts[i];
        }
    }

    public override void CloseUI()
    {
        _callback?.Invoke();
        base.CloseUI();
    }
}
