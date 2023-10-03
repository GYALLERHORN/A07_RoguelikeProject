using System;
using System.Collections;
using System.Collections.Generic;
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
    [Header("Sound Setting")]
    public float MasterVolume = 0.5f;
    public float BGMVolume = 0.5f;
    public float EffectVolume = 0.5f;
    public float UIVolume = 0.5f;
    public float OtherVolume = 0.5f;

}
