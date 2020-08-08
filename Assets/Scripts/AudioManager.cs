﻿using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] BGM;
    public Sound[] SFX;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    public string bgm;

    private void Awake()
    {
        instance = this;

        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        //Assign it Audio Source
        foreach (var bgm in BGM)
        {
            bgm.Source = bgmSource;
        }

        foreach (var sfx in SFX)
        {
            sfx.Source = sfxSource;
        }
    }

    private void Start()
    {
        PlayBGM(bgm);
    }

    public void PlayBGM(string name)
    {
        Sound s = null;

        try
        {
            s = Array.Find(BGM, sound => sound.Name == name);

            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.loop = s.Loop;

            s.Source.Play();
        }
        catch
        {
            Debug.LogError("Invalid Audio Name Input in PlayBGM()");
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = null;

        try
        {
            s = Array.Find(SFX, sound => sound.Name == name);

            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.loop = s.Loop;

            s.Source.Play();
        }
        catch
        {
            Debug.LogError("Invalid Audio Name Input in PlaySFX()");
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public void StopAll()
    {
        StopBGM();
        StopSFX();
    }
}
