using UnityEngine;

public class MascellaStunnedState : MascellaState
{
    private CapsuleCollider2D Body;
    [SerializeField] private CircleCollider2D Precausion;
    public override void OnEnter()
    {
        Debug.Log("Mascella is stunned");
        mascellaController.isMascellaCrashed = false;
        mascellaController.isMascellaStunned = true;
        Body = GetComponentInParent<CapsuleCollider2D>();
        Body.enabled = false;
        Precausion.enabled = true;
    }

    public override void OnUpdate()
    {
        if (mascellaController.mascellaDamageble.Health <= 0)
        PlayClipAtPosition(mascellaController.mascellaDeath, transform.position, 20f); 

    }
    public static void PlayClipAtPosition(AudioClip clip, Vector3 position, float volume = 10f)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = position;
        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.volume = volume;
        aSource.spatialBlend = 10f; // 3D sound
        aSource.Play();
        UnityEngine.Object.Destroy(tempGO, clip.length);
    }

    public override void OnExit()
    {
       
    }
    //private IEnumerator HandleStun()
    //{
    //    yield return new WaitForSeconds(mascellaStats.stunnedTime);
    //    mascellaVulnerable.ActivateWeakSpot(false);
    //    mascellaController.isMascellaStunned = false;
    //    mascellaController.isMascellaGetUp = true;
    //    yield return new WaitForSeconds(1f);
    //    mascellaController.isMascellaGetUp = false;
    //    mascellaController.MascellaSwitchState(mascellaController.mascellaIdleState);
    //}

}
