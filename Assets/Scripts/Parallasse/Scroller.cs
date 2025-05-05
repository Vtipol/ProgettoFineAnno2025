using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public enum ScrollMethod
    {
        Update,
        FixedUpdate,
        PlayerMovement
    }

    [SerializeField] public ScrollMethod scrollMethod; // Metodo di scrolling selezionabile
    [SerializeField] public RawImage _img;
    [SerializeField] public float _x, _y;
    [SerializeField] public GameObject player; // Riferimento al giocatore
    [SerializeField] public float scrollMultiplier = 0.1f; // Moltiplicatore per lo scrolling

    public Rigidbody2D playerRb;

    void Start()
    {
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb == null)
            {
                Debug.LogError("Il giocatore non ha un componente Rigidbody2D!");
            }
        }
        if (scrollMethod == ScrollMethod.PlayerMovement && player == null)
        {
            Debug.LogError("Il metodo di scrolling 'PlayerMovement' è selezionato ma il giocatore non è assegnato!");
        }
    }

    void Update()
    {
        if (scrollMethod == ScrollMethod.Update)
        {
            ScrollOnUpdate();
        }
        else if (scrollMethod == ScrollMethod.PlayerMovement)
        {
            ScrollOnPlayerMovement();
        }
    }

    void FixedUpdate()
    {
        if (scrollMethod == ScrollMethod.FixedUpdate)
        {
            ScrollOnFixedUpdate();
        }
    }

    void ScrollOnUpdate()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
    }

    void ScrollOnFixedUpdate()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.fixedDeltaTime, _img.uvRect.size);
    }

    void ScrollOnPlayerMovement()
    {
        if (playerRb != null)
        {
            // Usa solo la componente orizzontale della velocità del giocatore
            float playerVelocityX = playerRb.linearVelocity.x;

            // Calcola lo scrolling basato solo sulla velocità orizzontale
            _img.uvRect = new Rect(_img.uvRect.position + new Vector2(playerVelocityX * scrollMultiplier * Time.deltaTime, 0), _img.uvRect.size);
        }
    }
}


