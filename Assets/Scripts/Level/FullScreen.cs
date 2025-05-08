using UnityEngine;

public class FullScreen : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        DontDestroyOnLoad(gameObject);
    }
}
