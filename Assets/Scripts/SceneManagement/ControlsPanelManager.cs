using UnityEngine;

public class ControlsPanelManager : MonoBehaviour
{
    public GameObject controlsPanel;
    public Transform player;
    public float ledgeEndXPosition;
    
    private void Start()
    {
        // Show panel when scene loads
        ShowControlsPanel();
    }
    
    private void Update()
    {
        // Check if player has reached end of ledge
        if (player != null && player.position.x >= ledgeEndXPosition)
        {
            HideControlsPanel();
        }
    }
    
    public void ShowControlsPanel()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(true);
        }
    }
    
    public void HideControlsPanel()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
        }
    }
}