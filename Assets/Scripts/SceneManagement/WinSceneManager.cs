using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WinSceneManager : MonoBehaviour
{
    public TMP_Text finalTimeText;
    
    private void Start()
    {
        DisplayFinalTime();
    }
    
    private void DisplayFinalTime()
    {
        // Get final time from Timer class
        float finalTime = Timer.finalTime;
        
        // Format time
        TimeSpan timeSpan = TimeSpan.FromSeconds(finalTime);
        string timeText = string.Format("{0:00}:{1:00}.{2:000}", 
            timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        
        // Display on UI
        if (finalTimeText != null)
        {
            finalTimeText.text = "Your Time: " + timeText;
        }
    }
}