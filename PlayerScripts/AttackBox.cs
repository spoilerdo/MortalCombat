using UnityEngine;

public class AttackBox : MonoBehaviour
{
    private PlayerSetup playerSetup;
    private AttackBoxManager attackBoxManager;

    private AudioSource audioSource;
    public AudioClip Punch;

    private string PlayerName;

    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        playerSetup = GetComponentInParent<PlayerSetup>();
        attackBoxManager = GetComponentInParent<AttackBoxManager>();
        PlayerName = playerSetup.transform.name;
        Debug.Log("Attackbox: " + PlayerName);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player" && col.collider.name != PlayerName)
        {
            attackBoxManager.SetEnmeyName(col.collider.name);
            audioSource.PlayOneShot(Punch, 1);
        }
    }
}
