using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Pool;

public enum eSoundType
{
    Master,
    BGM,
    Effect,
    UI,
    Other,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private List<AudioClip> _BGM;
    /// <summary>
    /// 0 Master, 1 BGM, 2 Effect, 3 UI, 4 Other
    /// </summary>
    [SerializeField] private List<AudioMixerGroup> _audioMixer;
    [SerializeField] private float _changeDuration;
    private AudioSource _BGMAudioSource;
    private ObjectPool _pools;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _BGMAudioSource = GetComponent<AudioSource>();
            _pools = GetComponent<ObjectPool>();
        }
    }

    public static void ChangeBGM(int index)
    {
        if (index >= Instance._BGM.Count || index < 0)
            return;

        Instance.StartCoroutine(Instance.SlowlyChangeSound(index));
    }

    private IEnumerator SlowlyChangeSound(int index)
    {
        float time = 0.0f;
        float baseVolume = _BGMAudioSource.volume;
        while (time <= _changeDuration / 2f)
        {
            time += Time.deltaTime;
            _BGMAudioSource.volume = Mathf.Max(_BGMAudioSource.volume - baseVolume / _changeDuration, 0.0f);
            yield return null;
        }
        time = 0.0f;
        _BGMAudioSource.clip = _BGM[index];
        while (time <= _changeDuration / 2f)
        {
            time += Time.deltaTime;
            _BGMAudioSource.volume = Mathf.Min(_BGMAudioSource.volume + baseVolume / _changeDuration, baseVolume);
            yield return null;
        }
        yield break;
    }

    public class SoundData
    {
        public AudioClip Clip;
        public Vector3 Position;
        public float MinPitch;
        public float MaxPitch;
        public float Volume;
    }

    /// <summary>
    /// ������ ����� �׷��� ȿ������ ���带 ����ϴ� ���
    /// </summary>
    /// <param name="type">����� �ͼ� �׷�</param>
    /// <param name="clip">����� ����� ����</param>
    /// <param name="minPitch">���� �� ������ ����</param>
    /// <param name="maxPitch">���� �� ������ ����</param>
    public static void PlayClip(eSoundType type, AudioClip clip, float minPitch = 1.0f, float maxPitch = 1.0f)
    {
        GameObject obj = Instance._pools.SpawnFromPool(ePoolType.SoundSource);
        obj.SetActive(true);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        float volume;
        switch (type)
        {
            case eSoundType.Master:
                volume = DataManager.Instance.MasterVolume;
                break;
            case eSoundType.BGM:
                volume = DataManager.Instance.BGMVolume;
                break;
            case eSoundType.Effect:
                volume = DataManager.Instance.EffectVolume;
                break;
            case eSoundType.UI:
                volume = DataManager.Instance.UIVolume;
                break;
            case eSoundType.Other:
                volume = DataManager.Instance.OtherVolume;
                break;
            default:
                volume = 0;
                break;
        }
        soundSource.Play(Instance._audioMixer[(int)type], clip, volume, minPitch, maxPitch);
    }

    /// <summary>
    /// ������ ����� �׷�� ������ ������ ȿ���� �ְ� ���� ��쿡 ���.
    /// </summary>
    /// <param name="data">����) new SoundManager.SoundData() {Clip = Ŭ��, Position = this.transform Ȥ�� = Vector3.zero, MinPitch = 0.8f, MaxPitch = 1.2f, Volume = 0.3f };</param>
    public static void PlayClip(SoundData data)
    {
        GameObject obj = Instance._pools.SpawnFromPool(ePoolType.SoundSource);
        obj.SetActive(true);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(data);
    }
}
