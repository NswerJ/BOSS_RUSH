using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class AbillityUnlockText : MonoBehaviour
{
    [SerializeField] string _text;
    TextMeshPro _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    public void Show()
    {
        _textMeshPro.DOText(_text, _text.Length * 0.05f);
    }
}
