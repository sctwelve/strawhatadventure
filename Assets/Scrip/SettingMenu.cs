using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Animator volumeAnimator; // Referensi ke Animator Controller

    public void SetVolume(float volume)
    {
        // Mengkonversi nilai volume antara 0 hingga 1 menjadi nilai di rentang Audio Mixer
        float exposedValue = Mathf.Lerp(-80f, 0f, volume);
        audioMixer.SetFloat("volume", exposedValue);

        // Update parameter "VolumeLevel" pada Animator Controller
        volumeAnimator.SetFloat("VolumeLevel", volume);
    }

    public void DecreaseVolume(float amount)
    {
        audioMixer.GetFloat("volume", out float currentExposedValue);

        // Mengurangi nilai exposed parameter di Audio Mixer
        float newExposedValue = Mathf.Clamp(currentExposedValue - amount, -80f, 0f);
        audioMixer.SetFloat("volume", newExposedValue);

        // Update parameter "VolumeLevel" pada Animator Controller
        float normalizedValue = Mathf.InverseLerp(-80f, 0f, newExposedValue);
        volumeAnimator.SetFloat("VolumeLevel", normalizedValue);
    }

    public void IncreaseVolume(float amount)
    {
        audioMixer.GetFloat("volume", out float currentExposedValue);

        // Menambah nilai exposed parameter di Audio Mixer
        float newExposedValue = Mathf.Clamp(currentExposedValue + amount, -80f, 0f);
        audioMixer.SetFloat("volume", newExposedValue);

        // Update parameter "VolumeLevel" pada Animator Controller
        float normalizedValue = Mathf.InverseLerp(-80f, 0f, newExposedValue);
        volumeAnimator.SetFloat("VolumeLevel", normalizedValue);
    }
}
