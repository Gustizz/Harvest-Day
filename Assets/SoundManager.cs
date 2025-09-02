using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioSource _musicSource, _effectSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, float volumeScale, float pitch)
    {
        _effectSource.volume = volumeScale;
        _effectSource.pitch = pitch;
        _effectSource.PlayOneShot(clip);
    }

    public List<AudioClip> uiSounds = new List<AudioClip>();
    public List<AudioClip> enemyHit = new List<AudioClip>();

    public List<AudioClip> playerShoot = new List<AudioClip>();
    public List<AudioClip> shopClose = new List<AudioClip>();
    public List<AudioClip> fenceHit = new List<AudioClip>();
    public List<AudioClip> tomatoeSplat = new List<AudioClip>();

    public AudioClip cardsEntering;
    public AudioClip carrotHit;
    public AudioClip electricityFence;

    public AudioClip TransitionSwoosh;

    public AudioClip KeyboardType;

    public AudioClip RandomSoundFromClipList(List<AudioClip> clipList)
    {
        int soundIndex = Random.Range(0, clipList.Count-1);

        return clipList[soundIndex];
    }

    public void PlayButtonClick()
    {
        PlaySound(uiSounds[0], 0.1f, 1f);
    }
}
