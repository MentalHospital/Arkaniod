using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public enum Sound
    {
        PaddleHit,
        WallHit,
        BrickHit,
        BrickDestroyed,
        BallDestroyed
    }

    [SerializeField] private AudioSource source;
    [SerializeField] private SoundAudioTuple[] tuples;
    private Dictionary<Sound, AudioClip> soundAudios;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            source = GetComponent<AudioSource>();
            if (tuples == null)
                tuples = new SoundAudioTuple[0];
            soundAudios = new Dictionary<Sound, AudioClip>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void Start()
    {
        foreach(var tuple in tuples)
        {
            soundAudios.Add(tuple.sound, tuple.audioClip);
        }
    }

    private AudioClip GetClip(Sound sound)
    {
        var result = soundAudios[sound];
        if (result == null)
            Debug.LogError($"Sound {sound} was not found");
        return soundAudios[sound];
    }

    public void Play(Sound sound)
    {
        source.PlayOneShot(GetClip(sound));
    }
}
