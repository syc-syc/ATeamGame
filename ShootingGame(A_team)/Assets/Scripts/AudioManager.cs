using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    float masterVolumePercent = 1;
    float sfxVoluePercent = 1;
    //float musicVolumePercent = 1;

    public AudioSource gunEffect;

    public AudioClip ShootClip;
    public AudioClip RelodClip;

 
    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }
    }
    public void PlaySound(AudioClip clip,Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, sfxVoluePercent * masterVolumePercent);
    }
    public void Shoot(Vector3 pos)
    {
        PlaySound(ShootClip, pos);
    }
    public void Relod(Vector3 pos)
    {
        PlaySound(RelodClip, pos);
    }
}
