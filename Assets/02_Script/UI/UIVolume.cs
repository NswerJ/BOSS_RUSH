using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIVolume : MonoBehaviour
{
    [SerializeField] Slider _volumeSlider;
    [SerializeField] SoundType _soundType;
    TextMeshProUGUI _tmpro;

    private void Awake()
    {
        _tmpro = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        switch(_soundType)
        {
            case SoundType.Master:
                _volumeSlider.value = PlayerPrefs.GetFloat("MasterSound", 0.5f);
                break;
            case SoundType.BGM:
                _volumeSlider.value = PlayerPrefs.GetFloat("BGSound", 0.5f);
                break;
            case SoundType.SFX:
                _volumeSlider.value = PlayerPrefs.GetFloat("SFXSound", 0.5f);
                break;
        }
    }

    private void Update()
    {
        _tmpro.text = $"{(int)(_volumeSlider.value * 100)}";
        switch (_soundType)
        {
            case SoundType.Master:
                SoundManager.Instance.MasterSoundVolume(_volumeSlider.value);
                break;
            case SoundType.BGM:
                SoundManager.Instance.BGSoundVolume(_volumeSlider.value);
                break;
            case SoundType.SFX:
                SoundManager.Instance.SFXVolume(_volumeSlider.value);
                break;
        }
    }
}