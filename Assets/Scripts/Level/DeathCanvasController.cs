using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathCanvasController : MonoBehaviour
{
    [SerializeField] private Damageble playerDamageble;
    [SerializeField] private Canvas deathCanvas;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private bool hasDied = false;

    private void Start()
    {
        if (deathCanvas != null)
            deathCanvas.gameObject.SetActive(false);

        restartButton.onClick.AddListener(RestartLevel);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if (!hasDied && playerDamageble != null && !playerDamageble.IsAlive)
        {
            hasDied = true;
            Time.timeScale = 0f;
            deathCanvas.gameObject.SetActive(true);
        }
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
