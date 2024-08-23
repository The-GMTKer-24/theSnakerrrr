using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public static float volume = 1.0f;
    
    private Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
        volume = PlayerPrefs.GetFloat("Volume", volume);
        slider.value = volume;
    }

    // Update is called once per frame
    void Update()
    {
        volume = slider.value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
