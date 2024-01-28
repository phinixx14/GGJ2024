using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGSoundSource;
    public AudioClip TitleMusic;
    public AudioClip GameOverMusic;
    public AudioClip Stage1Music;
    public AudioClip CollisionSFX;
    public AudioClip LaunchNoseSFX;
    public AudioClip NoseHitSFX;
    public AudioClip DeathSFX;
    public AudioClip Squeak;

    List<AudioSource> audioSrcPool = new List<AudioSource>();

    public void Awake() {
        BGSoundSource.loop = true;
        for (int i = 0; i < 10; i++) {
            audioSrcPool.Add(gameObject.AddComponent<AudioSource>());
        }
    }
    public void PlayMusic(AudioClip sound) {
        PlayMusic(sound, true);
    }
    public void PlayMusic(AudioClip sound, bool retrigger) {
        if (BGSoundSource.clip == sound && !retrigger) {
            if (!BGSoundSource.isPlaying) {
                BGSoundSource.Play();
            }
        }
        else {
            BGSoundSource.clip = sound;
            BGSoundSource.Play();
        }
    }

    public void PlaySFX(AudioClip sound) {
        if (sound == LaunchNoseSFX) {
            AudioSource tgt = audioSrcPool[audioSrcPool.Count - 1];
            tgt.clip = sound;
            tgt.volume = .3f;
            tgt.Play();
            return;
        }

        AudioSource tgtSource = audioSrcPool.Find(src => !src.isPlaying);
        tgtSource.volume = 1;
        tgtSource.clip = sound;
        tgtSource.loop = false;
        tgtSource.PlayOneShot(sound);
    }
}
