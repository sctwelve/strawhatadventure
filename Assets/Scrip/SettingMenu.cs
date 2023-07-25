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
        audioMixer.SetFloat("volume", volume);

        // Update parameter "VolumeLevel" pada Animator Controller
        volumeAnimator.SetFloat("VolumeLevel", volume);
    }

    public void DecreaseVolume(float amount)
    {
        audioMixer.GetFloat("volume", out float currentVolume);
        audioMixer.SetFloat("volume", currentVolume - amount);
    }

    public void IncreaseVolume(float amount)
    {
        audioMixer.GetFloat("volume", out float currentVolume);
        audioMixer.SetFloat("volume", currentVolume + amount);
    }
}
