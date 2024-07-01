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

        foreach (GameObject soundSource in soundSources)
        {
            AudioSource[] audioSources = soundSource.GetComponents<AudioSource>();

            if (audioSources != null)
            {
                foreach(var audioSource in audioSources)
                {
                    AudioSources.Add(audioSource);
                }
            }
        }

        if (AudioSources == null)
            Debug.LogError("AudioSource is not Given");

        foreach (AudioSource source in AudioSources)
        {
            if(source.clip != null && source.clip.name == "conveyor-belt") {
                source.volume = GameSettings.Instance.volume / 100f * 0.75f;
            }
            else if(source.clip != null && source.clip.name == "cat-happy" || source.clip.name == "cat-neutral" || source.clip.name == "cat-angry" || source.clip.name == "cat-super") {
                source.volume = GameSettings.Instance.volume / 100f * 0.825f;
            }
            else {
                source.volume = GameSettings.Instance.volume / 100f;
            }
        }

    }

    public void ChangeVolume()
    {
        foreach (AudioSource source in AudioSources)
        {
            if(source.clip != null && source.clip.name == "conveyor-belt") {
                source.volume = GameSettings.Instance.volume / 100f * 0.75f;
            }
            else if(source.clip != null && source.clip.name == "cat-happy" || source.clip.name == "cat-neutral" || source.clip.name == "cat-angry" || source.clip.name == "cat-super") {
                source.volume = GameSettings.Instance.volume / 100f * 0.825f;
            }
            else {
                source.volume = GameSettings.Instance.volume / 100f;
            }
        }
    }
}
