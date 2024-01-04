using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    [Header("Info")]
    [SerializeField]
    private RectTransform _creditTrm;

    [SerializeField]
    private TextMeshProUGUI _creditText;

    [Header("Text")]
    [SerializeField]
    private List<string> _textList = new List<string>();
    [SerializeField]
    private List<float> _textFirstWaitTime = new List<float>();
    [SerializeField]
    private List<float> _textWaitTime = new List<float>();

    [SerializeField]
    private List<TextMeshProUGUI> _textOutList = new List<TextMeshProUGUI>();
    private List<string> _textOutTextList = new List<string>();

    [SerializeField]
    private List<float> _textOutYValue = new List<float>();

    int _index = 0;

    [Header("Scroll")]
    [SerializeField] private float _endY;
    [SerializeField] private float _speed;

    [SerializeField] private float _scrollY;
    [SerializeField] private bool _firstTextSkip;

    private bool _accel = false;

    [Header("Sound")]
    [SerializeField]
    private AudioClip _textClip;

    private void Awake()
    {
        for(int i = 0; i < _textOutList.Count; ++i)
        {
            _textOutTextList.Add(_textOutList[i].text);
            _textOutList[i].text = "";
        }


        StartCoroutine(CreditCo());
    }

    private void Update()
    {
        _accel = (Input.anyKey);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_creditTrm == null) return;

        _creditTrm.anchoredPosition = Vector2.up * _scrollY;
    }
#endif

    IEnumerator CreditCo()
    {
        if(!_firstTextSkip)
        {
            for (int i = 0; i < _textList.Count; i++)
            {
                _creditText.text = "";
                yield return new WaitForSeconds(_textFirstWaitTime[i]);
                for (int j = 0; j < _textList[i].Length; j++)
                {
                    SoundManager.Instance.SFXPlay("TextOut", _textClip);
                    _creditText.text += _textList[i][j];

                    yield return new WaitForSeconds(_accel ? 0.05f : 0.1f);
                }

                yield return new WaitForSeconds(_textWaitTime[i]);
            }
        }

        while(_creditTrm.anchoredPosition.y <= _endY)
        {
            _scrollY += _accel ? _speed * Time.deltaTime * 2 : _speed * Time.deltaTime;
            _creditTrm.anchoredPosition = (Vector2.up * _scrollY);
            if(_index < _textOutYValue.Count && _creditTrm.anchoredPosition.y >= _textOutYValue[_index])
            {
                StartCoroutine(DoText(_index));
                _index++;
            }

            yield return null;
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    IEnumerator DoText(int index)
    {
        float tipingSpeed = _textOutTextList[index].Length > 50 ? 0.05f : 0.1f;

        for (int j = 0; j < _textOutTextList[index].Length; j++)
        {
            SoundManager.Instance.SFXPlay("TextOut", _textClip);

            _textOutList[index].text += _textOutTextList[index][j];

            yield return new WaitForSeconds(_accel ? tipingSpeed / 2 : tipingSpeed);
        }
    }
   
}
