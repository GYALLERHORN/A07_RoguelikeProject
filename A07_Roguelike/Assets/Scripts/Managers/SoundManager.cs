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
    /// 정해진 오디오 그룹의 효과에서 사운드를 재생하는 경우
    /// </summary>
    /// <param name="type">오디오 믹서 그룹</param>
    /// <param name="clip">재생할 오디오 파일</param>
    /// <param name="minPitch">랜덤 음 높낮이 범위</param>
    /// <param name="maxPitch">랜덤 음 높낮이 범위</param>
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
    /// 정해진 오디오 그룹과 별개로 볼륨과 효과를 주고 싶은 경우에 사용.
    /// </summary>
    /// <param name="data">예시) new SoundManager.SoundData() {Clip = 클립, Position = this.transform 혹은 = Vector3.zero, MinPitch = 0.8f, MaxPitch = 1.2f, Volume = 0.3f };</param>
    public static void PlayClip(SoundData data)
    {
        GameObject obj = Instance._pools.SpawnFromPool(ePoolType.SoundSource);
        obj.SetActive(true);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(data);
    }
}
