using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InteractPrompt : MonoBehaviour
{
    public GameObject promptUI; // The UI panel or text holder
    public TextMeshProUGUI promptText;
    public string actionName = "Interact"; // Input action name

    private PlayerInput playerInput;
    private InputAction interactAction;
    private bool isPlayerInRange = false;

    private void Start()
    {
        promptUI.SetActive(false);

        playerInput = FindFirstObjectByType<PlayerInput>(); 
        interactAction = playerInput.actions[actionName];

        playerInput.onControlsChanged += UpdatePromptKey;
        UpdatePromptKey(playerInput); // Initial display
    }

    private void OnDestroy()
    {
        playerInput.onControlsChanged -= UpdatePromptKey;
    }

    private void UpdatePromptKey(PlayerInput input)
    {
        string keyDisplay = "[Interact]";
        var bindingIndex = interactAction.GetBindingIndexForControl(interactAction.controls[0]);

        if (bindingIndex != -1)
        {
            keyDisplay = interactAction.GetBindingDisplayString(bindingIndex);
        }

        promptText.text = $"{keyDisplay} to interact";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            promptUI.SetActive(true);
            UpdatePromptKey(playerInput);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            promptUI.SetActive(false);
        }
    }
}
