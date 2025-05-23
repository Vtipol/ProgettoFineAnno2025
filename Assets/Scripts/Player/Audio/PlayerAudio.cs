using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footstepClip;
    public AudioClip attackClip;

    public void PlayFootstep()
    {
        audioSource.PlayOneShot(footstepClip);
    }

    public void PlayAttack()
    {
        audioSource.PlayOneShot(attackClip);
    }
}
