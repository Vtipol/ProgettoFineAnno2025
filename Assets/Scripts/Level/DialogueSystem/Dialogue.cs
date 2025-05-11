using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI textConponent;
    public string[] lines;
    public float textSpeed;

    private int index;

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
                StopAllCoroutines();
                textConponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textConponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false); 
        }
    }

    #region Timers

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textConponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed); 
        }
    }

    #endregion
}
