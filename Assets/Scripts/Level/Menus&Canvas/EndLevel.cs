using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private Canvas endLevelCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (endLevelCanvas != null)
            {
                endLevelCanvas.gameObject.SetActive(true);
            }

            //Pause the game
            Time.timeScale = 0;
        }
    }
}
