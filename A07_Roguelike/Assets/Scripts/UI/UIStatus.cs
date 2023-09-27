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
        sb.AppendLine($"ü�� : {_character.CurrentStates.maxHealth}");
        sb.AppendLine($"�ӵ� : {_character.CurrentStates.speed}");
        if (_character.CurrentStates.attackSO != null)
        {
            if (_character.CurrentStates.attackSO is RangedAttackData)
            {
                RangedAttackData data = _character.CurrentStates.attackSO as RangedAttackData;
                sb.AppendLine($"����ü : {data.bulletNameTag}");
                sb.AppendLine($"���ӽð� : {data.duration}");
                sb.AppendLine($"�� �� �߻�ü ���� : {data.numberofProjectilesPerShot}");
                float angle = data.spread + 0.5f * data.numberofProjectilesPerShot * data.multipleProjectilesAngle;
                sb.AppendLine($"���� : -{angle}��~ +{angle}��");
            }
            sb.AppendLine($"����ũ�� : x{_character.CurrentStates.attackSO.size}");
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
}
