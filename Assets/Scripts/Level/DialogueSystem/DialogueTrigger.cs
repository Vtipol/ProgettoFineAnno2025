using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueUI; // Dialogue canvas object
    public Dialogue dialogueScript; // Reference to Dialogue script

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && InputManager.InteractWasPressed)
        {
            dialogueUI.SetActive(true);
            dialogueScript.enabled = true; // This will trigger Start() in Dialogue
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
