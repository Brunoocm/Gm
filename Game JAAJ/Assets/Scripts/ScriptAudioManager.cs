using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class ScriptAudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static ScriptAudioManager instance;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        //FindObjectOfType<ScriptAudioManager>().Play("NOME"); EXEMPLO
        //FindObjectOfType<ScriptAudioManager>().Play("NOME"); EXEMPLO
        //FindObjectOfType<ScriptAudioManager>().Play("NOME"); EXEMPLO
    }

    void Start()
    {
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Som: " + name + " não encontrado!");
            return;
        }

        s.source.Play();
    }
}
