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
    private float TimeBeforComboEnds = 2f;
    private int MaxAmountCombos = 3;
    private float DamageMultiplier = 2f;
    //private float WaitForEndAnimation = 0.1f;

    [HideInInspector]
    public string EnemyName;

    void Start()
    {
        DefaultDamage = Damage;
    }
    [Client]
    public void SetEnmeyName(string _Enemyname)
    {
        EnemyName = _Enemyname;
        //PlayerController.IsDamaged = true;
        SetCombo();
    }
    void SetCombo()
    {
        if (Combo < MaxAmountCombos)
        {
            Damage = Damage * DamageMultiplier + Combo;
            Debug.LogError("Combo is being used" + Combo);
            Combo++;
        }
        StartCoroutine(ComboDuration());
        CmdPlayerHit(EnemyName);
    }
    //TODO The !localplayer is getting dubble damage fix it!!! (not always)
    [Command]
    void CmdPlayerHit(string _PlayerID)
    {
        Player player = GameManager.GetPlayer(_PlayerID);
        player.TakeDamage(Damage);
        Damage = DefaultDamage;
    }
    private IEnumerator ComboDuration()
    {
        //Combo = -1;
        yield return new WaitForSeconds(TimeBeforComboEnds);
        //Debug.Log("Waited for " + TimeBeforComboEnds);
        //Debug.Log("Combo = " + Combo);
        Combo = -1;
        Damage = DefaultDamage;
    }
}
