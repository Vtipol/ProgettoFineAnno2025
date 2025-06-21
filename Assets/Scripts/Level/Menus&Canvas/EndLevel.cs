using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndLevel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Canvas endLevelCanvas;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private Button returnToMenuButton;

    [Header("Typing")]
    [TextArea(2, 6)]
    [SerializeField] private string[] endLines;
    [SerializeField] private float textSpeed = 0.05f;

    private int index = 0;
    private Coroutine typingCoroutine;
    private bool isTypingComplete = false;

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

        if (endLevelCanvas != null)
        {
            endLevelCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!endLevelCanvas.gameObject.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTypingComplete)
            {
                ShowNextLine();
            }
            else
            {
                if (typingCoroutine != null)
                    StopCoroutine(typingCoroutine);

                endText.text = endLines[index];
                isTypingComplete = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            index = 0;
            endLevelCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
            typingCoroutine = StartCoroutine(TypeLine());
        }
    }

    private void ShowNextLine()
    {
        if (index < endLines.Length - 1)
        {
            index++;
            endText.text = string.Empty;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            returnToMenuButton.gameObject.SetActive(true);
        }
    }

    private IEnumerator TypeLine()
    {
        isTypingComplete = false;
        endText.text = "";

        string line = endLines[index];
        int i = 0;

        while (i < line.Length)
        {
            // If we hit a rich text tag like <b>, <color=...>, etc.
            if (line[i] == '<')
            {
                int tagCloseIndex = line.IndexOf('>', i);
                if (tagCloseIndex != -1)
                {
                    // Append the whole tag instantly
                    string tag = line.Substring(i, tagCloseIndex - i + 1);
                    endText.text += tag;
                    i = tagCloseIndex + 1;
                    continue;
                }
            }

            endText.text += line[i];

            // Optional: extra delay for punctuation
            if (line[i] == '.' || line[i] == ',' || line[i] == '\n')
                yield return new WaitForSecondsRealtime(textSpeed * 4);
            else
                yield return new WaitForSecondsRealtime(textSpeed);

            i++;
        }

        isTypingComplete = true;
    }


    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
