using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _pressKey;
    private Sequence _pressKeyAnim;
    [SerializeField] private TMP_Text _title;
    [Header("메뉴 선택")]
    [SerializeField] private GameObject _menu;
    [SerializeField] private AudioClip _clickClip;
    private eMenuType _selection = eMenuType.Start;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private enum eMenuType
    {
        Start,
        Load,
        Exit,
    }

    private void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        SoundManager.ChangeBGM(0);

        FadeLoopTxt(_pressKey);
        CharAnim(_title, _title.text.Length / 2);
    }

    private void Update()
    {
        if (!_menu.activeInHierarchy && Input.anyKeyDown)
        {
            _menu.SetActive(true);
            _pressKey.gameObject.SetActive(false);
            _pressKeyAnim.Pause();
            SoundManager.PlayClip(eSoundType.UI, _clickClip);
        }
        else
        {
            if (/*Input.GetKeyDown(KeyCode.Space) ||*/ Input.GetKeyDown(KeyCode.Return))
            {
                SoundManager.PlayClip(eSoundType.UI, _clickClip);
                switch (_selection)
                {
                    case eMenuType.Start:
                        StartBtn();
                        break;
                    case eMenuType.Load:
                        LoadBtn();
                        break;
                    case eMenuType.Exit:
                        ExitBtn();
                        break;
                }
            }
            else if (Input.anyKeyDown)
            {
                SoundManager.PlayClip(eSoundType.UI, _clickClip);
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    OffImgae(_menu.transform.GetChild((int)_selection).GetChild(0).GetComponent<Image>());
                    _selection = _selection - 1 < 0 ? eMenuType.Exit : _selection - 1;
                    OnImage(_menu.transform.GetChild((int)_selection).GetChild(0).GetComponent<Image>());
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    OffImgae(_menu.transform.GetChild((int)_selection).GetChild(0).GetComponent<Image>());
                    _selection = _selection + 1 > eMenuType.Exit ? eMenuType.Start : _selection + 1;
                    OnImage(_menu.transform.GetChild((int)_selection).GetChild(0).GetComponent<Image>());
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_menu.activeInHierarchy)
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            if (results.Count > 0)
            {
                //Debug.Log("Hit " + results[0].gameObject.transform.parent.GetSiblingIndex());
                if ((int)_selection != results[0].gameObject.transform.parent.GetSiblingIndex())
                {
                    OffImgae(_menu.transform.GetChild((int)_selection).GetChild(0).GetComponent<Image>());
                    _selection = (eMenuType)results[0].gameObject.transform.parent.GetSiblingIndex();
                    OnImage(_menu.transform.GetChild((int)_selection).GetChild(0).GetComponent<Image>());
                }
            }
        }
    }

    private void OffImgae(Image image)
    {
        var col = image.color;
        col.a = 0;
        image.color = col;
    }

    private void OnImage(Image image)
    {
        var col = image.color;
        col.a = 0.8f;
        image.color = col;
    }

    private void FadeLoopTxt(TMP_Text text)
    {
        _pressKeyAnim = DOTween.Sequence();
        _pressKeyAnim.Append(text.DOFade(.0f, 2f));
        _pressKeyAnim.Append(text.DOFade(1.0f, 2f));
        _pressKeyAnim.SetLoops(-1);
        _pressKeyAnim.Play();
    }

    private void CharAnim(TMP_Text text, float duration, AudioClip clip = null)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x =>
        {
            text.maxVisibleCharacters = (int)x;
        }
        , 0f, text.text.Length, duration).SetEase(Ease.Linear);
    }

    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadBtn()
    {

    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
