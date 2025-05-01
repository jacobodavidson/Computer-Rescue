using UnityEngine;
using UnityEngine.SceneManagement;

public class ComputerInteraction : MonoBehaviour
{
    [SerializeField] private string winSceneName = "Win";
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that entered the trigger is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop timer before loading win scene
            if (Timer.instance != null)
            {
                Timer.instance.StopTimer();
            }

            // Load the win scene
            SceneManager.LoadScene(winSceneName);
        }
    }
}