using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    // References for pause menu
    [Header("Menu Elements")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Button quitButton;
    
    // Scene to load when quitting
    [SerializeField] private string titleSceneName = "Title";
    
    // Reference to Input Actions asset
    [SerializeField] private InputActionAsset inputActions;
    private InputAction pauseAction;
    
    // Tracking variables
    private bool isPaused = false;
    private float previousTimeScale;
    
    void Awake()
    {
        // Find Pause action in UI action map
        pauseAction = inputActions.FindActionMap("UI").FindAction("Pause");
        
        // Register callback for pause action
        pauseAction.performed += ctx => TogglePauseMenu();
    }
    
    void OnEnable()
    {
        // Enable pause action
        pauseAction?.Enable();
    }
    
    void OnDisable()
    {
        // Disable pause action
        pauseAction?.Disable();
    }
    
    void Start()
    {
        if (pauseMenuPanel == null)
        {
            return;
        }
        
        // Ensure menu starts disabled
        pauseMenuPanel.SetActive(false);
        
        // Setup button listeners
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(ReturnToTitleScreen);
        }
        
        // Initialize sliders
        InitializeVolumeSliders();
    }
    
    private void InitializeVolumeSliders()
    {
        // Initialize music volume slider
        if (musicVolumeSlider != null)
        {
            // Set initial value from saved data
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        
        // Initialize SFX volume slider
        if (sfxVolumeSlider != null)
        {
            // Set initial value from saved data
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }
    
    // Handles music volume changes
    private void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        
        // Apply volume change immediately
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(volume);
        }
    }
    
    // Handles SFX volume changes
    private void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        
        // Implement this in AudioManager later
    }
    
    public void TogglePauseMenu()
    {
        // Check if pauseMenuPanel still exists
        if (pauseMenuPanel == null)
        {
            return;
        }

        isPaused = !isPaused;
        
        if (isPaused)
        {
            // Store current time scale
            previousTimeScale = Time.timeScale;
            
            // Pause game
            Time.timeScale = 0f;
            
            // Show pause menu panel
            pauseMenuPanel.SetActive(true);
            
            // Make cursor visible and unlock it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Unpause game
            Time.timeScale = previousTimeScale;
            
            // Hide pause menu panel
            pauseMenuPanel.SetActive(false);
        }
    }
    
    public void SaveSettings()
    {
        // Save settings to PlayerPrefs
        PlayerPrefs.Save();
    }
    
    void ReturnToTitleScreen()
    {
        // Resume normal time before loading
        Time.timeScale = 1f;
        
        // Load title screen
        SceneManager.LoadScene(titleSceneName);
    }
    
    public bool IsMenuActive()
    {
        return isPaused;
    }
    
    private void OnApplicationQuit()
    {
        // Save settings when application quits
        SaveSettings();
    }
}