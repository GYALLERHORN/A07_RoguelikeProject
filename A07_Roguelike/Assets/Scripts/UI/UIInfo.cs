using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/// <summary>
/// ĳ���� ���� �� ������ ���� ǥ�ø� ���� UI 
/// </summary>
public class UIInfo : UIBase
{
    eInfoType _infoType;
    private CharacterStatsHandler _character;
    // private ������ ���� Ŭ����

    private bool IsTemp;
    private float _time;
    private float _duration;
    [Header("������ ǥ�õǴ� ������Ʈ")]
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

    public void Initialize(Sprite icon, /*������ ������ ���� Ŭ���� �ޱ�, */Vector3 position, Action actAtHide = null, bool temp = false, float duration = .0f)
    {
        _icon.sprite = icon;
        // ������ ���� �ޱ�

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
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"ü�� : {_character.CurrentStates.maxHealth}");
        sb.AppendLine($"�ӵ� : {_character.CurrentStates.speed}");
        if (_character.CurrentStates.attackSO != null)
        {
            if (_character.CurrentStates.attackSO is RangedAttackData)
            {
                RangedAttackData data = _character.CurrentStates.attackSO as RangedAttackData;
                sb.AppendLine($"����ü : {data.bulletNameTag}");
                sb.AppendLine($"���ӽð� : {data.duration}��");
                sb.AppendLine($"�� �� �߻�ü ���� : {data.numberofProjectilesPerShot}��");
                float angle = data.spread + 0.5f * data.numberofProjectilesPerShot * data.multipleProjectilesAngle;
                sb.AppendLine($"���� : -{angle}��  ~  +{angle}��");
            }
            sb.AppendLine($"����ũ�� : x{_character.CurrentStates.attackSO.size.ToString("F2")}");
            sb.AppendLine($"�������� : {_character.CurrentStates.attackSO.delay}��");
            sb.AppendLine($"���ݷ� : {_character.CurrentStates.attackSO.power}");
            sb.AppendLine($"���ݼӵ� : {_character.CurrentStates.attackSO.speed}");

            if (_character.CurrentStates.attackSO.isOnKnockback)
            {
                sb.AppendLine($"�˹鼼�� : {_character.CurrentStates.attackSO.knockbackPower}");
                sb.AppendLine($"�˹�ð� : {_character.CurrentStates.attackSO.knockbackTime}");
            }
        }
        return sb.ToString();
    }

    private string MakeItemInfo()
    {
        StringBuilder sb = new StringBuilder();
        // ������ ���� ���ڿ� �����

        return sb.ToString();
    }
}
