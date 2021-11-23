using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.name = sound.name;
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void Play(string name, bool setRandomPitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Audio not found");
            return;
        }
        if (setRandomPitch)
        {
            s.source.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        }
        s.source.Play();
        }

    public void Play(string name)
    {
        Play(name, false);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Audio not found");
            return;
        }
        s.source.Stop();
    }
}
