using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Needed for Button

public class EndLevel : MonoBehaviour
{
    [SerializeField] private Canvas endLevelCanvas;
    [SerializeField] private Button returnToMenuButton;

    private void Awake()
    {
        if (returnToMenuButton != null)
        {
            returnToMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (endLevelCanvas != null)
            {
                endLevelCanvas.gameObject.SetActive(true);
            }

            Time.timeScale = 0; // Pause the game
        }
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Unpause before switching scenes
        SceneManager.LoadScene("MainMenu");
    }
}
