using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMenu : UIBase
{
    [SerializeField] private RectTransform _menuRoot;
    [Header("�ִϸ��̼��� ���� ���")]
    [SerializeField] private RectTransform _menuFrame;
    private UIInfo _statusUI;

    private void Start()
    {
        Invoke("Initialize", 1f);
    }

    public void Initialize()
    {
        AddMenu("����â", () =>
        {
            // var ui = UIManager.ShowUI(eUIType.Status) as UIInfo;
            // _statusUI = ui;
            // _statusUI.Initialize();
        });
        AddMenu("�ٽ� ����", () =>
        {
            // LoadScene("MainScene");
        });
        AddMenu("����", () =>
        {

        });

        _menuFrame.DOSizeDelta(Vector2.zero, 0.4f).SetEase(Ease.OutBack);
    }

    public UIMenuItem AddMenu(string title, Action openAtClick)
    {
        var menu = UIManager.ShowUI<UIMenuItem>(_menuRoot);
        menu.Initialize(title, openAtClick);
        return menu;
    }


}
