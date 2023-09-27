using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UIStatus : UIBase
{
    private CharacterStatsHandler _character;
    private Action _callback;
    private bool IsTemp;
    private float _time;
    private float _duration;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _icon;

    private void LateUpdate()
    {
        if (IsTemp)
        {
            _time += Time.deltaTime;
            if (_duration < _time)
                CloseUI();
        }
    }

    public void Initialize(Sprite icon, CharacterStatsHandler stats, Action callback = null, bool temp = false, float duration = 0.0f)
    {
        _character = stats;
        _callback = callback;
        IsTemp = temp;
        _time = 0.0f;
        _duration = duration;
        _icon.sprite = icon;
        _text.text = MakeStatInfo();
    }

    public override void Refresh()
    {
        base.Refresh();
        _text.text = MakeStatInfo();
    }

    public override void CloseUI()
    {
        _callback?.Invoke();
        base.CloseUI();
    }

    private string MakeStatInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"체력 : {_character.CurrentStates.maxHealth}");
        sb.AppendLine($"속도 : {_character.CurrentStates.speed}");
        if (_character.CurrentStates.attackSO != null)
        {
            if (_character.CurrentStates.attackSO is RangedAttackData)
            {
                RangedAttackData data = _character.CurrentStates.attackSO as RangedAttackData;
                sb.AppendLine($"투사체 : {data.bulletNameTag}");
                sb.AppendLine($"지속시간 : {data.duration}");
                sb.AppendLine($"샷 당 발사체 개수 : {data.numberofProjectilesPerShot}");
                float angle = data.spread + 0.5f * data.numberofProjectilesPerShot * data.multipleProjectilesAngle;
                sb.AppendLine($"각도 : -{angle}°~ +{angle}°");
            }
            sb.AppendLine($"공격크기 : x{_character.CurrentStates.attackSO.size}");
            sb.AppendLine($"공격지연 : {_character.CurrentStates.attackSO.delay}초");
            sb.AppendLine($"공격력 : {_character.CurrentStates.attackSO.power}");
            sb.AppendLine($"공격속도 : {_character.CurrentStates.attackSO.speed}");

            if (_character.CurrentStates.attackSO.isOnKnockback)
            {
                sb.AppendLine($"넉백세기 : {_character.CurrentStates.attackSO.knockbackPower}");
                sb.AppendLine($"넉백시간 : {_character.CurrentStates.attackSO.knockbackTime}");
            }
        }
        return sb.ToString();
    }
}
