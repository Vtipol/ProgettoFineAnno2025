using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    [Header("Footsteps Array")]
    public AudioClip[] footstepClips;
    [Header("Jump Sound")]
    public AudioClip jumpClip;
    [Header("Landing Array")]
    public AudioClip[] landingClips;
    [Header("Attack Sound")]
    public AudioClip attackClip;

    public void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        int index = Random.Range(0, footstepClips.Length);
        AudioClip clipToPlay = footstepClips[index];
        audioSource.PlayOneShot(clipToPlay);
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpClip);
    }

    public void PlayLanding()
    {
        if (landingClips.Length == 0) return;

        int index = Random.Range(0, landingClips.Length);
        AudioClip clipToPlay = landingClips[index];
        audioSource.PlayOneShot(clipToPlay);
    }

    public void PlayAttack()
    {
        audioSource.PlayOneShot(attackClip);
    }
}
