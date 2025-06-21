using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI textConponent;
    public string[] lines;
    public float textSpeed = 0.05f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] typewriterClips;

    private int index;
    private Coroutine typingCoroutine;
    private bool isLineFullyTyped;

    private void OnEnable()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        index = 0;
        textConponent.text = string.Empty;
        Time.timeScale = 0f;
        typingCoroutine = StartCoroutine(TypeLine());
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isLineFullyTyped)
            {
                NextLine();
            }
            else
            {
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                    typingCoroutine = null;
                }

                textConponent.text = lines[index];
                isLineFullyTyped = true;
            }
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textConponent.text = string.Empty;

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void RestartDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        index = 0;
        textConponent.text = string.Empty;
        typingCoroutine = StartCoroutine(TypeLine());
        gameObject.SetActive(true);
    }

    IEnumerator TypeLine()
    {
        isLineFullyTyped = false;
        typingCoroutine = null;

        foreach (char c in lines[index])
        {
            textConponent.text += c;

            if (!char.IsWhiteSpace(c))
            {
                PlayTypingSound();
            }

            yield return new WaitForSecondsRealtime(textSpeed);
        }

        isLineFullyTyped = true;
    }

    void PlayTypingSound()
    {
        if (typewriterClips.Length > 0 && audioSource != null)
        {
            AudioClip clip = typewriterClips[Random.Range(0, typewriterClips.Length)];
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(clip);
        }
    }
}
