using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class DataManager
{
    private static DataManager _instance;
    public static DataManager Instance { get => Get(); }
    private static DataManager Get()
    {
        if (_instance == null)
            _instance = new DataManager();
        return _instance;
    }

    public float MasterVolume
    {
        get => _masterVol;
        set
        {
            _masterVol = value;
            SoundManager.ChangeBGMVol();
        }
    }
    public float BGMVolume
    {
        get => _BGMVol;
        set
        {
            _BGMVol = value;
            SoundManager.ChangeBGMVol();
        }
    }
    public float EffectVolume { get => _effectVol; set => _effectVol = value; }
    public float UIVolume { get => _UIVol; set => _UIVol = value; }
    public float OtherVolume { get => _otherVol; set => _otherVol = value; }

    private float _masterVol = 0.5f;
    private float _BGMVol = 0.5f;
    private float _effectVol = 0.5f;
    private float _UIVol = 0.5f;
    private float _otherVol = 0.5f;
}
