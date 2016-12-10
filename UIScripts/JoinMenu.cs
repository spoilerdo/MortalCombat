using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinMenu : MonoBehaviour
{
    //private NetworkLobbyManager manager;   
    private NetworkManager manager;

    private string Adress;
    private string SceneName;

    private GameObject PlayerPrefab;

    void Awake()
    {
        //manager = FindObjectOfType<NetworkLobbyManager>();
        manager = FindObjectOfType<NetworkManager>();
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    /*public void SelectPlayerNumber(int _PlayerNumber)
    {
        AddPlayer.PlayerNumber = _PlayerNumber;
    }*/
    public void SelectPlayer(GameObject _Player)
    {
        PlayerPrefab = _Player;
    }
    public void SetAdress(string _Adress)
    {
        Adress = _Adress;
    }
    public void Join()
    {
        SceneManager.LoadScene("Level01");
        manager.playerPrefab = PlayerPrefab;
        //manager.gamePlayerPrefab = PlayerPrefab;
        manager.networkAddress = Adress;
        manager.StartClient();
    }
}
