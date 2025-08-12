using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip musicIdle;
    public AudioClip musicPlaying;
    public AudioClip musicDead;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip sfxStart;
    public AudioClip sfxJump;
    public AudioClip sfxScore;
    public AudioClip sfxCrash;

    [Header("Music Fades")]
    [Range(0f, 2f)] public float fadeTime = 0.25f;  // quick, subtle fade

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        // Ensure playOnAwake is off; we drive everything from code.
        if (musicSource != null) { musicSource.playOnAwake = false; musicSource.loop = true; }
        if (sfxSource != null) { sfxSource.playOnAwake = false; }
    }

    public void PlayMusic(AudioClip clip, bool loop = true, float volume = 1f)
    {
        if (clip == null || musicSource == null) return;

        // If same clip & state, no churn
        if (musicSource.clip == clip && musicSource.isPlaying && musicSource.loop == loop) return;

        StopAllCoroutines(); // cancel any previous fades
        StartCoroutine(FadeSwapMusic(clip, loop, volume));
    }

    public void StopMusic() { if (musicSource != null) musicSource.Stop(); }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null) sfxSource.PlayOneShot(clip, volume);
    }

    private IEnumerator FadeSwapMusic(AudioClip next, bool loop, float targetVol)
    {
        if (musicSource.clip != null && musicSource.isPlaying && fadeTime > 0f)
        {
            float t = 0f; float start = musicSource.volume;
            while (t < fadeTime) { t += Time.unscaledDeltaTime; musicSource.volume = Mathf.Lerp(start, 0f, t / fadeTime); yield return null; }
        }

        musicSource.Stop();
        musicSource.clip = next;
        musicSource.loop = loop;
        musicSource.volume = 0f;
        musicSource.Play();

        if (fadeTime > 0f)
        {
            float t = 0f;
            while (t < fadeTime) { t += Time.unscaledDeltaTime; musicSource.volume = Mathf.Lerp(0f, targetVol, t / fadeTime); yield return null; }
        }
        else
        {
            musicSource.volume = targetVol;
        }
    }
}
