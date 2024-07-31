using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    Toggle toggle;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGM_slider, SFX_slider;

    public void Start()
    {
        toggle = transform.Find("InvertYToggle").GetComponent<Toggle>();
        toggle.isOn = PlayerPrefs.GetInt("isInverted", 0) == 1;
        BGM_slider.value = PlayerPrefs.GetFloat("BGM_volume", 0);
        SFX_slider.value = PlayerPrefs.GetFloat("SFX_volume", 0);
    }
    public void Back()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("previous_scene"));
    }
    public void Apply()
    {

        float bgmVolume = Mathf.Lerp(-80f, 20f, BGM_slider.value);
        float sfxVolume = Mathf.Lerp(-80f, 20f, SFX_slider.value);

        PlayerPrefs.SetInt("isInverted", toggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("BGM_volume", BGM_slider.value);
        PlayerPrefs.SetFloat("SFX_volume", SFX_slider.value);
        audioMixer.SetFloat("BGM", bgmVolume);
        audioMixer.SetFloat("SFX", sfxVolume);
        SceneManager.LoadScene(PlayerPrefs.GetString("previous_scene"));
    }
}
