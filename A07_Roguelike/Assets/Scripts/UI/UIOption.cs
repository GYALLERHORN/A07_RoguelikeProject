using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public struct AudioVolume
{
    public float Master;
    public float BGM;
    public float Effect;
    public float UI;
    public float Other;
}
[Serializable]
public struct CustomResolution
{
    public int width;
    public int height;
}
public class UIOption : UIBase
{
    private AudioVolume _Volume;
    private AudioVolume _preVolume;

    [Header("표시할 해상도 리스트")]
    [SerializeField] private List<CustomResolution> _ResolutionListForUI;
    private List<Resolution> _ResolutionList = new List<Resolution>();
    private int _ResolutionIndex;
    private Resolution _Resolution;
    private int _preResolutionIndex;

    private List<Display> _OutputDisplayList;
    private int _OutputDisplayIndex;
    private Display _OutputDisplay;
    private Display _preOutputDisplay;

    private bool _FullScreenMode;
    private bool _preFullScreenMode;

    private bool _isChanged = false;

    [Header("각 옵션별 component")]
    [SerializeField] private Slider _MasterVolSlider;
    [SerializeField] private TMP_Text _MasterVolTxt;

    [SerializeField] private Slider _BGMVolSlider;
    [SerializeField] private TMP_Text _BGMVolTxt;

    [SerializeField] private Slider _EffectVolSlider;
    [SerializeField] private TMP_Text _EffectVolTxt;

    [SerializeField] private Slider _UIVolSlider;
    [SerializeField] private TMP_Text _UIVolTxt;

    [SerializeField] private Slider _OtherVolSlider;
    [SerializeField] private TMP_Text _OtherVolTxt;

    [SerializeField] private TMP_Dropdown _ResolutionDropdown;
    [SerializeField] private TMP_Dropdown _outputDisplayDropdown;
    [SerializeField] private TMP_Dropdown _DisplayMode;

    public void Initialize(Action actAtClose)
    {
        ActAtClose = actAtClose;

        _Volume.Master = DataManager.Instance.MasterVolume;
        _Volume.BGM = DataManager.Instance.BGMVolume;
        _Volume.Effect = DataManager.Instance.EffectVolume;
        _Volume.UI = DataManager.Instance.UIVolume;
        _Volume.Other = DataManager.Instance.OtherVolume;

        _preVolume = _Volume;

        foreach (var resol in _ResolutionListForUI)
        {
            var item = new Resolution() { width = resol.width, height = resol.height };
            _ResolutionList.Add(item);
        }

        _Resolution = Screen.currentResolution;
        _ResolutionIndex = _ResolutionList.FindIndex(x =>
        {
            if (x.width == _Resolution.width && x.height == _Resolution.height)
                return true;
            return false;
        });
        _preResolutionIndex = _ResolutionIndex;

        _OutputDisplay = Display.main;
        _preOutputDisplay = _OutputDisplay;
        _OutputDisplayList = new List<Display>();
        _OutputDisplayList.AddRange(Display.displays);
        _OutputDisplayIndex = _OutputDisplayList.FindIndex(x =>
        {
            if (x.Equals(_OutputDisplay))
                return true;
            return false;
        });

        _FullScreenMode = Screen.fullScreen;
        _preFullScreenMode = _FullScreenMode;

        _ResolutionDropdown.ClearOptions();
        foreach (var opt in _ResolutionList)
        {
            TMP_Dropdown.OptionData item = new TMP_Dropdown.OptionData();
            item.text = $"{opt.width}x{opt.height}";
            _ResolutionDropdown.options.Add(item);
        }

        _outputDisplayDropdown.ClearOptions();
        foreach (var opt in _OutputDisplayList)
        {
            TMP_Dropdown.OptionData item = new TMP_Dropdown.OptionData();
            item.text = $"{opt}";
            _outputDisplayDropdown.options.Add(item);
        }

        Refresh();
    }

    public override void Refresh()
    {
        base.Refresh();
        _MasterVolSlider.value = _Volume.Master;
        _MasterVolTxt.text = _Volume.Master.ToString("F2");

        _BGMVolSlider.value = _Volume.BGM;
        _BGMVolTxt.text = _Volume.BGM.ToString("F2");

        _EffectVolSlider.value = _Volume.Effect;
        _EffectVolTxt.text = _Volume.Effect.ToString("F2");

        _UIVolSlider.value = _Volume.UI;
        _UIVolTxt.text = _Volume.UI.ToString("F2");

        _OtherVolSlider.value = _Volume.Other;
        _OtherVolTxt.text = _Volume.Other.ToString("F2");

        _ResolutionDropdown.value = _ResolutionIndex;
        _outputDisplayDropdown.value = _OutputDisplayIndex;
        _DisplayMode.value = _FullScreenMode ? 0 : 1;
    }

    public void SaveOption()
    {
        DataManager.Instance.MasterVolume = _Volume.Master;
        DataManager.Instance.BGMVolume = _Volume.BGM;
        DataManager.Instance.EffectVolume = _Volume.Effect;
        DataManager.Instance.UIVolume = _Volume.UI;
        DataManager.Instance.OtherVolume = _Volume.Other;

        _preVolume = _Volume;
        _preResolutionIndex = _ResolutionIndex;
        _preOutputDisplay = _OutputDisplay;
        _preFullScreenMode = _FullScreenMode;

        var ui = UIManager.ShowUI<UIPopup>();
        if (ui != null)
        {
            ui.Initialize("설정이 저장되었습니다.", null, null, true, 1f);
        }

        _isChanged = false;
    }

    public void CancelOption()
    {
        if (_isChanged)
        {
            _Volume = _preVolume;

            _FullScreenMode = _preFullScreenMode;
            SetResolution(_preResolutionIndex);
            _ResolutionIndex = _ResolutionList.FindIndex(x =>
            {
                if (x.width == _Resolution.width && x.height == _Resolution.height)
                    return true;
                return false;
            });

            _OutputDisplay = _preOutputDisplay;
            _OutputDisplayIndex = _OutputDisplayList.FindIndex(x =>
            {
                if (x.Equals(_preOutputDisplay))
                    return true;
                return false;
            });
            SetTargetDisplay(_OutputDisplayIndex);
        }
        else
            SelfCloseUI();
    }

    public void SetMasterVol(float vol)
    {
        _Volume.Master = vol;
        _isChanged = true;
        _MasterVolTxt.text = _Volume.Master.ToString("F2");
    }

    public void SetBGMVol(float vol)
    {
        _Volume.BGM = vol;
        _isChanged = true;
        _BGMVolTxt.text = _Volume.BGM.ToString("F2");
    }

    public void SetEffectVol(float vol)
    {
        _Volume.Effect = vol;
        _isChanged = true;
        _EffectVolTxt.text = _Volume.Effect.ToString("F2");
    }

    public void SetUIVol(float vol)
    {
        _Volume.UI = vol;
        _isChanged = true;
        _UIVolTxt.text = _Volume.UI.ToString("F2");
    }

    public void SetOtherVol(float vol)
    {
        _Volume.Other = vol;
        _isChanged = true;
        _OtherVolTxt.text = _Volume.Other.ToString("F2");
    }

    public void SetTargetDisplay(int index)
    {
        var cam = Camera.main;
        cam.targetDisplay = index;
        _isChanged = true;
    }

    public void SetResolution(int index)
    {
        Resolution resolution = _ResolutionList[index];
        Screen.SetResolution(resolution.width, resolution.height, _FullScreenMode);
        _Resolution = resolution;
        _isChanged = true;
    }

    public void SetFullScreenMode(int screenMode)
    {
        bool isFullScreen;
        if (screenMode == 0)
            isFullScreen = true;
        else
            isFullScreen = false;
        Screen.SetResolution(_Resolution.width, _Resolution.height, isFullScreen);
        _FullScreenMode = isFullScreen;
        _isChanged = true;
    }
}
