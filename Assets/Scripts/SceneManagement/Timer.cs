using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public TMP_Text timerText;
    private float timeElapsed = 0f;
    private bool isRunning = false;
    
    // Static variable to store final time for access in Win scene
    public static float finalTime = 0f;

    private void Awake()
    {
        // Singleton pattern to access timer from other scripts
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Start timer when the game begins
        StartTimer();
    }

    private void Update()
    {
        if (isRunning)
        {
            // Update time elapsed
            timeElapsed += Time.deltaTime;
            
            // Format and display time
            DisplayTime();
        }
    }

    private void DisplayTime()
    {
        // Convert to TimeSpan for easy formatting
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeElapsed);
        
        // Format as minutes:seconds.milliseconds
        string timeText = string.Format("{0:00}:{1:00}.{2:000}", 
            timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        
        // Update UI text
        if (timerText != null)
        {
            timerText.text = "Time: " + timeText;
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
        
        // Store final time for use in Win scene
        finalTime = timeElapsed;
    }
}