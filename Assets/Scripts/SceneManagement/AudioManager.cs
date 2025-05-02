using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] musicTracks;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private float crossFadeDuration = 1.0f;

    // Sound effect properties
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip jumpSound;
    
    private AudioSource crossFadeSource;
    private int currentSceneIndex = -1;
    private bool isCrossFading = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Initialize secondary audio source for crossfading
            crossFadeSource = gameObject.AddComponent<AudioSource>();
            crossFadeSource.loop = true;
            crossFadeSource.volume = 0;
            crossFadeSource.playOnAwake = false;
            
            // Initialize SFX source if not assigned
            if (sfxSource == null)
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
                sfxSource.loop = false;
                sfxSource.playOnAwake = false;
            }
            
            // Register for scene load events
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        // Unregister from scene load events when destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int newSceneIndex = scene.buildIndex;
        
        // Check if in title scene
        if (scene.name == "Title")
        {
            PlayMusic(menuMusic);
            return;
        }
        
        // If track for this scene, play it
        if (newSceneIndex < musicTracks.Length && musicTracks[newSceneIndex] != null)
        {
            PlayMusic(musicTracks[newSceneIndex]);
        }
        
        currentSceneIndex = newSceneIndex;
    }
    
    public void PlayMusic(AudioClip newTrack)
    {
        // If already playing this track, do nothing
        if (musicSource.clip == newTrack && musicSource.isPlaying)
        {
            return;
        }
        
        // If already cross-fading, stop that coroutine
        if (isCrossFading)
        {
            StopAllCoroutines();
            isCrossFading = false;
        }
        
        // If not playing anything yet, just start playing
        if (musicSource.clip == null || !musicSource.isPlaying)
        {
            musicSource.clip = newTrack;
            musicSource.Play();
            return;
        }
        
        // Otherwise, crossfade to new track
        StartCoroutine(CrossFadeMusic(newTrack));
    }
    
    private IEnumerator CrossFadeMusic(AudioClip newTrack)
    {
        isCrossFading = true;
        
        // Set up crossfade source with new track
        crossFadeSource.clip = newTrack;
        crossFadeSource.volume = 0;
        crossFadeSource.Play();
        
        float startVolume = musicSource.volume;
        
        // Fade out current track and fade in new track
        for (float t = 0; t < crossFadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, t / crossFadeDuration);
            crossFadeSource.volume = Mathf.Lerp(0, startVolume, t / crossFadeDuration);
            yield return null;
        }
        
        // Swap audio sources
        AudioSource temp = musicSource;
        musicSource = crossFadeSource;
        crossFadeSource = temp;
        
        // Stop old track
        crossFadeSource.Stop();
        crossFadeSource.clip = null;
        crossFadeSource.volume = 0;
        
        isCrossFading = false;
    }
    
    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }

    // Play jump sound
    public void PlayJumpSound()
    {
        if (jumpSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(jumpSound);
        }
    }
    
    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
        }
    }
}