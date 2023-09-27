using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusTest : MonoBehaviour
{
    [Header("ǥ�� ����(�ʼ�)")]
    [SerializeField] private GameObject player;
    private CharacterStatsHandler characterController;
    private Sprite icon;
    [Header("���� ǥ���� ������Ʈ")]
    [SerializeField] private Toggle isTemp;
    [SerializeField] private TMP_InputField duration;
    [SerializeField] private Button button;
    private bool _isTemp = false;
    private float _duration = .0f;
    private UIStatus _ui;

    private void Awake()
    {
        duration.gameObject.SetActive(false);
        characterController = player.GetComponent<CharacterStatsHandler>();
        icon = player.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public void SetIsTemp(bool isTemp)
    {
        Debug.Log($"Set Temp => {isTemp}");
        _isTemp = isTemp;
        if (_isTemp)
        {
            _duration = 0;
            duration.gameObject.SetActive(true);
        }
        else
        {
            _duration = 0;
            duration.text = "";
            duration.gameObject.SetActive(false);
        }
    }

    public void SetDuration(string duration)
    {
        if (Single.TryParse(duration, out float time))
        {
            _duration = time;
        }
    }

    public void Refresh()
    {
        if (characterController == null || icon == null)
            return;
        
        if (_ui == null)
            return;

        _ui.Initialize(icon,
            characterController,
            new Vector3(UnityEngine.Random.Range(-800,800), UnityEngine.Random.Range(-300, 300), 0),
            () =>
            {
                //button.interactable = true;
                Debug.Log("Status UI �����.");
            },
            _isTemp, _duration);
    }

    public void MakeUI()
    {
        if (characterController == null || icon == null)
            return;

        //button.interactable = false;
        _ui = UIManager.ShowUI(eUIType.Status) as UIStatus;
        if (_ui == null)
            return;

        _ui.Initialize(icon,
            characterController,
            Vector3.zero,
            () =>
            {
                //button.interactable = true;
                Debug.Log("Status UI �����.");
            },
            _isTemp, _duration);
    }
}
