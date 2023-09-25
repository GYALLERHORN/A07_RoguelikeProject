using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataManager
{
    private static DataManager instance;
    public static DataManager Get()
    {
        if (instance == null)
            instance = new DataManager();
        return instance;
    }
    [Header("Sound Setting")]
    public float MasterVolume;
    public float BGMVolume;
    public float EffectVolume;
    public float UIVolume;


}
