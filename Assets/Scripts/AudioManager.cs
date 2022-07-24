using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    public Sound[] sounds;
    public bool stopAudio = false;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        loadAudioSources();
        DontDestroyOnLoad(instance);
    }

    void loadAudioSources() {
        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void play(string name) {
        if (stopAudio) {
            return;
        }

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Sound " + name + " not found");
            return;
        }
        s.source.Play();
    }
    
    public void stop(string name) {
        if (stopAudio) {
            return;
        }
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Sound " + name + " not found");
            return;
        }
        if (s.source.isPlaying) {
            s.source.Stop();
        }
    }

    public void pause(string name) {
        if (stopAudio) {
            return;
        }
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Sound " + name + " not found");
            return;
        }
        if (s.source.isPlaying) {
            s.source.Pause();
        }
    }

    public void resume(string name) {
        if (stopAudio) {
            return;
        }
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Sound " + name + " not found");
            return;
        }
        if (s.source.isPlaying) {
            s.source.Play();
        }
    }

    public bool isPlaying(string name) {
        if (stopAudio) {
            return false;
        }
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Sound " + name + " not found");
            return false;
        }
        return s.source.isPlaying;
    }

    void Start() {
        
    }

    void Update() {
        
    }
}
