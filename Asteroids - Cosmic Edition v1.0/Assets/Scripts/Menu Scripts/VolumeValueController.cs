using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValueController : MonoBehaviour
{

    private AudioSource audiosrc;
    public float musicVolume = 1f;

    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }
    void Update()
    {
        audiosrc.volume = musicVolume;
    }
    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
