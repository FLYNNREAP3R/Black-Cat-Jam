using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    private List<AudioSource> AudioSources = new List<AudioSource>();
    [SerializeField] Slider volumeSlider;

    private void Start()
    {
        GameObject[] soundSources = GameObject.FindGameObjectsWithTag("SoundSource");
        Debug.Log(soundSources.Length);

        foreach (GameObject soundSource in soundSources)
        {
            AudioSource audioSource = soundSource.GetComponent<AudioSource>();

            if (audioSource != null)
                AudioSources.Add(audioSource);
        }

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
