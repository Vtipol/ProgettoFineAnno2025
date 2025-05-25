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

    private void OnEnable()
    {
        textConponent.text = string.Empty;
        Time.timeScale = 0f;
        StartDialogue();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textConponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }

                textConponent.text = lines[index];
                PlayTypingSound(); // Play one final sound
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        typingCoroutine = StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textConponent.text = string.Empty;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textConponent.text += c;

            if (!char.IsWhiteSpace(c)) // Skip sound for spaces, tabs, etc.
            {
                PlayTypingSound();
            }

            yield return new WaitForSecondsRealtime(textSpeed);
        }
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
