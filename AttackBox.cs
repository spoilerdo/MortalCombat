using UnityEngine;

public class AttackBox : MonoBehaviour
{
    private PlayerSetup playerSetup;
    private AttackBoxManager attackBoxManager;

    private string PlayerName;

    void Start()
    {
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
        }
    }
}
