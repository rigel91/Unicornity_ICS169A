using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioSource sfx;
    public static AudioManager instance;

    //For Randomization in Pitch (Player Voice)
    public float lowPitchRange = 1.2f;
    public float highPitchRange = 1.5f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        //This will change.. will house the games overall theme music for start-up menu.
        Play("Village");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void RandomizeSfx(params AudioClip [] clips)
    {
        int randomIndex = UnityEngine.Random.Range(0, clips.Length);
        float randomPitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);

        //Assignment
        sfx.pitch = randomPitch;
        sfx.clip = clips[randomIndex];
        sfx.Play();
    }
}
