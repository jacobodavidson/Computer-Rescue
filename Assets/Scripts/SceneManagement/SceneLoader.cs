using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    
    private Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        
        if (button != null)
        {
            button.onClick.AddListener(LoadTargetScene);
        }
    }
    
    public void LoadTargetScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}