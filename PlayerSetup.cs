using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] ComponendsToDisable;

	void Start () {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < ComponendsToDisable.Length; i++)
            {
                ComponendsToDisable[i].enabled = false;
            }
        }
	}
    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        GameManager.RegisterPlayer(netID, player);
    }
}
