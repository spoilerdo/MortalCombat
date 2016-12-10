using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] ComponendsToDisable;

    //private PlayerController playerController;

    /*void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }*/
	void Start () {
        for (int i = 0; i < ComponendsToDisable.Length; i++)
        {
            ComponendsToDisable[i].enabled = false;
        }
    }
    void Update()
    {
        if(MatchController.GameStarts == true)
        {
            EnablePlayers();
        }
    }
    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        GameManager.RegisterPlayer(netID, player);
    }
    public void EnablePlayers()
    {
        if(MatchController.GameStarts == true && isLocalPlayer)
        {
            for (int i = 0; i < ComponendsToDisable.Length; i++)
            {
                ComponendsToDisable[i].enabled = true;
            }
        }
    }
}
