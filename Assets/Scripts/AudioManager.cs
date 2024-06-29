using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> AudioSources = new List<AudioSource>();
    [SerializeField] Slider volumeSlider;

    private void Start()
    {
        if (AudioSources == null)
            Debug.LogError("AudioSource is not Given");

        foreach (AudioSource source in AudioSources)
        {
            source.volume = GameSettings.Instance.volume / 100f;
        }

    }

    public void ChangeVolume()
    {
        foreach (AudioSource source in AudioSources)
        {
            source.volume = volumeSlider.value;
        }
    }
}
