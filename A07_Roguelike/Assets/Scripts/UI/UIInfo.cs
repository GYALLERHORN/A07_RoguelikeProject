using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/// <summary>
/// 캐릭터 스탯 및 아이템 정보 표시를 위한 UI 
/// </summary>
public class UIInfo : UIBase
{
    eInfoType _infoType;
    private CharacterStatsHandler _character;
    // private 아이템 정보 클래스

    private bool IsTemp;
    private float _time;
    private float _duration;
    [Header("내용이 표시되는 오브젝트")]
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _icon;

    private bool isFollow;
    private Transform _follow;

    private void FixedUpdate()
    {
        if (isFollow)
        {
            Move(Camera.main.WorldToScreenPoint(_follow.position));
        }
    }

    private void LateUpdate()
    {
        if (IsTemp)
        {
            _time += Time.deltaTime;
            if (_duration < _time)
                SelfHideUI();
        }
    }

    private enum eInfoType
    {
        Stats,
        Item,
    }

    public void Initialize(Sprite icon, CharacterStatsHandler stats, Vector3 position, Action actAtHide = null, bool temp = false, float duration = 0.0f)
    {
        _icon.sprite = icon;
        _character = stats;

        transform.localPosition = position;

        ActAtHide = actAtHide;
        IsTemp = temp;
        _time = 0.0f;
        _duration = duration;

        _text.text = MakeStatInfo();
        _infoType = eInfoType.Stats;

        isFollow = false;
        _follow = null;
    }

    public void Initialize(Sprite icon, /*아이템 정보에 대한 클래스 받기, */Vector3 position, Action actAtHide = null, bool temp = false, float duration = .0f)
    {
        _icon.sprite = icon;
        // 아이템 정보 받기

        transform.localPosition = position;

        ActAtHide = actAtHide;
        IsTemp = temp;
        _time = 0.0f;
        _duration = duration;

        _text.text = MakeStatInfo();
        _infoType = eInfoType.Item;

        isFollow = false;
        _follow = null;
    }

    public void SetFollow(Transform world)
    {
        isFollow = true;
        _follow = world;
    }

    public void Move(Vector3 screen)
    {
        transform.localPosition = screen;
    }

    public override void Refresh()
    {
        base.Refresh();
        switch (_infoType)
        {
            case eInfoType.Item:
                _text.text = MakeItemInfo();
                break;
            case eInfoType.Stats:
                _text.text = MakeStatInfo();
                break;
            default:
                break;
        }
    }

    public override void CloseUI()
    {
        isFollow = false;
        _follow = null;

        base.CloseUI();
    }

    public override void HideUI()
    {
        isFollow = false;
        _follow = null;

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

    private string MakeStatInfo()
    {
        string info = _character.GetInfoString();
        return info;
    }

    private string MakeItemInfo()
    {
        StringBuilder sb = new StringBuilder();
        // 아이템 정보 문자열 만들기

        return sb.ToString();
    }
}
