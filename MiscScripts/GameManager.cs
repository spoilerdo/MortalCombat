using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private static Dictionary<string, Player> Players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string _NetID, Player _Player)
    {
        string PlayerID = "Player " + _NetID;
        Players.Add(PlayerID, _Player);
        _Player.transform.name = PlayerID;
    }
    public static void DeRegisterPlayer(string _PlayerID) //wil je de functie roepen gebruik dan transform.name = _PlayerID
    {
        Players.Remove(_PlayerID);
    }
    public static Player GetPlayer(string _PlayerID)
    {
        return Players[_PlayerID];
    }
}
