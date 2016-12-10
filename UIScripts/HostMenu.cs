using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class HostMenu : MonoBehaviour {

    public Animator ImageButtons;
    public Animator PlayerSelectPanel;

    public int State = 0;
    [HideInInspector]
    public static string SceneName;

    private GameObject PlayerPrefab;
    //private NetworkLobbyManager manager;
    private NetworkManager manager;

    public Text StatusText;

    void Awake()
    {
        //manager = FindObjectOfType<NetworkLobbyManager>();
        manager = FindObjectOfType<NetworkManager>();
        StatusText.text = Network.player.ipAddress;
    }
    public void RightArrow()
    {
        if(State != 4)
        {
            State++;
            ImageButtons.SetInteger("State", State);
        }
    }
    public void LeftArrow()
    {
        if(State != 0)
        {
            State--;
            ImageButtons.SetInteger("State", State);
        }
    }
    public void BackButton1()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void BackButton2()
    {
        PlayerSelectPanel.SetBool("IsTriggered", false);
    }
    public void Loadlevel(string _Level)
    {
        SceneName = _Level;
        PlayerSelectPanel.SetBool("IsTriggered", true);
    }
    public void SelectPlayer(GameObject _Player)
    {
        PlayerPrefab = _Player;
    }
    public void StartUpHost()
    {
        manager.ServerChangeScene("Lobby");
        manager.onlineScene = SceneName;
        manager.playerPrefab = PlayerPrefab;
        //manager.gamePlayerPrefab = PlayerPrefab;
        manager.StartHost();
    }
}
