using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button returnToTitleButton;
    [SerializeField] private Button loadCheckpointButton;
    [SerializeField] private Button quitButton;
    
    [SerializeField] private OutOfBound outOfBoundScript; 

    private bool isPaused = false;

    private void Start()
    {
        pauseCanvas.gameObject.SetActive(false);

        continueButton.onClick.AddListener(ResumeGame);
        loadCheckpointButton.onClick.AddListener(LoadCheckpoint);
        returnToTitleButton.onClick.AddListener(ReturnToTitle);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        isPaused = false;
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    private void LoadCheckpoint()
    {
        Time.timeScale = 1f;
        outOfBoundScript.LoadCheckpoint();
        ResumeGame(); 
    }

    private void ReturnToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
