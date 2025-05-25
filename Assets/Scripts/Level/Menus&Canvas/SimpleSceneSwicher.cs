using UnityEngine;

public class SimpleSceneSwicher : MonoBehaviour
{
    private void Awake()
    {
        
    }

    [SerializeField] string sceneName;
    public void ChangeScene()
    {
        LevelManager.Instance.ChangeScene(sceneName);
    }

    public void AddScene()
    {
        LevelManager.Instance.AddScene(sceneName);
    }

    public void Quit()
    {
        LevelManager.Instance.QuitGame();
    }
}
