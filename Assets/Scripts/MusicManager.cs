using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void ChangeVolume(Slider slider)
    {
        audioSource.volume = slider.value;
    }

}
