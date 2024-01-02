using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TxtSize : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshProUGUI;
    CustomButton _button;

    private void Awake()
    {
        _button = GetComponent<CustomButton>();
    }

    private void Update()
    {
        if(_button.IsHover)
        {
            _textMeshProUGUI.fontSize = 76;
        }
        else
        {
            _textMeshProUGUI.fontSize = 52;
        }
    }
}