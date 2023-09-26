using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupTest : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text data;
    [SerializeField] private Toggle isTemp;
    [SerializeField] private TMP_Text duration;
    [SerializeField] private Button button;
    private string _title;
    private string _data;
    private bool _isTemp = false;
    private float _duration = .0f;

    public void SetTitle(string title)
    {
        _title = title;
    }

    public void SetData(string data)
    {
        _data = data;
    }

    public void SetIsTemp(bool isTemp)
    {
        _isTemp = isTemp;
        if (_isTemp)
        {
            _duration = 0;
            duration.text = "";
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
        if (_title == null || _data == null)
            return;

        //button.interactable = false;
        var ui = UIManager.Instance.ShowUI<UIPopup>(eUIType.Popup);
        if (ui == null)
            return;

        ui.Initialize(_data, _title, () => {
            //button.interactable = true;
            Debug.Log("Á¾·áµÊ.");
        }, _isTemp, _duration);
    }
}
