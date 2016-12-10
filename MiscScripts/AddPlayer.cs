using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class AddPlayer : NetworkManager
{
    public GameObject[] Players;

    public static int PlayerNumber; //Dit gebruiken om en niet een string.

    public class NetworkMessage : MessageBase
    {
        public int ChosenNumber;
    }
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int SlectedNumber = message.ChosenNumber;
        //Debug.Log(SlectedNumber);
        GameObject Player = Instantiate(Players[SlectedNumber]) as GameObject;
        //Debug.Log(Player);
        NetworkServer.AddPlayerForConnection(conn, Player, playerControllerId);
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.ChosenNumber = PlayerNumber;
        ClientScene.AddPlayer(conn, 0, test);
    }
    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }
    public void SetPlayerNumber(int _Number)
    {
        PlayerNumber = _Number;
    }
}
