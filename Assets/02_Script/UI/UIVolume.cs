using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIVolume : MonoBehaviour
{
    [SerializeField] Slider _volumeSlider;
    TextMeshProUGUI _tmpro;

    private void Awake()
    {
        _tmpro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _tmpro.text = $"{(int)(_volumeSlider.value * 100)}";
    }
}