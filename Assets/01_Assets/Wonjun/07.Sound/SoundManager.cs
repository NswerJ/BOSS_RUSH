using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum SoundType
{
    Master,
    BGM,
    SFX
}

public class SoundManager : MonoBehaviour
{
    public AudioMixer _mixer;
    public AudioSource _bgSound;
    public AudioClip[] _bgList;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            SceneManager.sceneLoaded += OnSceneLoded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < _bgList.Length; i++)
        {
            if (arg0.name == _bgList[i].name)
                BgSoundPlay(_bgList[i]);

        }
    }

    public void MasterSoundVolume(float val)
    {
        _mixer.SetFloat("Master", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("MasterSound",val);
    }

    public void BGSoundVolume(float val)
    {
        _mixer.SetFloat("BGSound", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("BGSound",val);
    }

    public void SFXVolume(float val)
    {
        _mixer.SetFloat("SFXvolume", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("SFXSound",val);
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    public void BgSoundPlay(AudioClip clip)
    {
        _bgSound.outputAudioMixerGroup = _mixer.FindMatchingGroups("BGSound")[0];
        _bgSound.clip = clip;
        _bgSound.loop = true;
        _bgSound.Play();
    }

}

