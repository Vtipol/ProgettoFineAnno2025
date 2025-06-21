using Unity.Cinemachine;
using UnityEngine;

public class EndLevelCamera : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public CinemachineCamera cinemachineCamera;

    [Header("Settings")]
    public float LookOffset = 2f;   
    public float smoothSpeed = 5f;

    private float direction = 1f;
    private Vector3 originalOffset;
    private Vector3 targetOffset;
    private CinemachineFollow cinemachineFollow;

    void Start()
    {
        if (cinemachineCamera == null)
        {
            Debug.LogError("CinemachineCamera reference not assigned.");
            enabled = false;
            return;
        }

        // Get the CinemachineFollow component
        cinemachineFollow = cinemachineCamera.GetComponent<CinemachineFollow>();
        if (cinemachineFollow == null)
        {
            Debug.LogError("CinemachineFollow component not found on CinemachineCamera.");
            enabled = false;
            return;
        }

        originalOffset = cinemachineFollow.FollowOffset;
        targetOffset = originalOffset;
    }

    void Update()
    {
        if (player == null) return;

        UpdateDirection();
    }

    void UpdateDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
            direction = Mathf.Sign(horizontal);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float desiredYOffset = LookOffset;

        Vector3 currentOffset = cinemachineFollow.FollowOffset;
        targetOffset = new Vector3(desiredYOffset, originalOffset.y, originalOffset.z);

        cinemachineFollow.FollowOffset = Vector3.Lerp(currentOffset, targetOffset, Time.deltaTime * smoothSpeed);
    }
}
