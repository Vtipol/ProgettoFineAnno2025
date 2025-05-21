using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndLevel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Canvas endLevelCanvas;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private TextMeshProUGUI endText;

    [Header("Typing Effect")]
    [TextArea]
    [SerializeField] private string[] endLines;
    [SerializeField] private float textSpeed = 0.05f;

    private int index;

    private void Awake()
    {
        if (returnToMenuButton != null)
        {
            returnToMenuButton.onClick.AddListener(ReturnToMainMenu);
        }

        if (endText != null)
        {
            endText.text = string.Empty;
        }
    }

    private void Update()
    {
        if (!endLevelCanvas.gameObject.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (endText.text == endLines[index])
            {
                index++;
                if (index < endLines.Length)
                {
                    endText.text = "";
                    StartCoroutine(TypeLine());
                }
            }
            else
            {
                StopAllCoroutines();
                endText.text = endLines[index];
            }
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

            Time.timeScale = 0f;
            index = 0;
            StartCoroutine(TypeLine());
        }
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator TypeLine()
    {
        if (endText == null || endLines.Length == 0) yield break;

        endText.text = "";

        foreach (char c in endLines[index].ToCharArray())
        {
            endText.text += c;
            yield return new WaitForSecondsRealtime(textSpeed); // Use WaitForSecondsRealtime because Time.timeScale is 0
        }
    }
}
