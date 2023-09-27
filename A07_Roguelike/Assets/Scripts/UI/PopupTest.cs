using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupTest : MonoBehaviour
{
    [SerializeField] private TMP_InputField title;
    [SerializeField] private TMP_InputField data;
    [SerializeField] private Toggle isTemp;
    [SerializeField] private TMP_InputField duration;
    [SerializeField] private Button button;
    private string _title = null;
    private string _data = null;
    private bool _isTemp = false;
    private float _duration = .0f;

    private void Awake()
    {
        duration.gameObject.SetActive(false);
    }

    public void SetTitle(string title)
    {
        if (title == "")
            _title = null;
        else
            _title = title;
    }

    public void SetData(string data)
    {
        if (data == "")
            _data = null;
        else
            _data = data;
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

    public void MakePopup()
    {
        if (_data == null)
            return;

        //button.interactable = false;
        var ui = UIManager.ShowUI(eUIType.Popup) as UIPopup;
        if (ui == null)
            return;
        
        ui.Initialize(_data, _title, () => {
            //button.interactable = true;
            Debug.Log("Á¾·áµÊ.");
        }, _isTemp, _duration);
    }
}
