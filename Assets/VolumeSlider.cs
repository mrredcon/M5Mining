using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text percentText;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeSetting;
    [SerializeField] private float defaultVolume = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //audioMixer.GetFloat(volumeSetting, out float currentVolume);
        //slider.value = currentVolume;
        slider.value = PlayerPrefs.GetFloat(volumeSetting);
        if (slider.value == 0) {
            slider.value = defaultVolume;
        }
        SetVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume()
    {
        float decVolume = Mathf.Log10(slider.value) * 20;
        if (slider.value == 0) {
            decVolume = -80;
        }

        percentText.text = (int)(slider.value * 100.0) + "%";
        audioMixer.SetFloat(volumeSetting, decVolume);
        PlayerPrefs.SetFloat(volumeSetting, slider.value);
    }
}
