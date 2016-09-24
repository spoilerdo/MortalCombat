using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class AttackBoxManager : NetworkBehaviour {

    [SerializeField]
    private float Damage = 1;
    private float DefaultDamage;

    //Combo variables
    [SyncVar]
    public int Combo = -1;
    private float TimeBeforComboEnds = 1.5f;
    private int MaxAmountCombos = 3;
    private float DamageMultiplier = 2f;
    private float WaitForEndAnimation = 0.1f;

    private string EnemyName;

    void Start()
    {
        DefaultDamage = Damage;
    }
    [Client]
    public void SetEnmeyName(string _Enemyname)
    {
        EnemyName = _Enemyname;
        PlayerController.IsDamaged = true;
        SetCombo();
    }
    void SetCombo()
    {
        if (Combo > 0)
        {
            Damage = Damage * DamageMultiplier * Combo;
            Debug.LogError("Combo is being used" + Combo);
        }
        else
        {
            StartCoroutine(ComboDuration());
        }
        if (Combo < MaxAmountCombos)
        {
            Combo++;
        }
        CmdPlayerHit(EnemyName);
    }
    //TODO The !localplayer is getting dubble damage fix it!!! (not always)
    [Command]
    void CmdPlayerHit(string _PlayerID)
    {
        Player player = GameManager.GetPlayer(_PlayerID);
        player.TakeDamage(Damage);
    }
    private IEnumerator ComboDuration()
    {
        yield return new WaitForSeconds(TimeBeforComboEnds);
        Debug.Log("Waited for " + TimeBeforComboEnds);
        Combo = -1;
        Debug.Log("Combo = " + Combo);
        Damage = DefaultDamage;
    }
}
