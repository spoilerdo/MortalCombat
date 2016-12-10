using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LobbyManager : NetworkBehaviour
{
    private NetworkManager manager;

    //private bool ClientConnected = false;

    [SyncVar]
    public float WaitingTime = 11;
    [SyncVar]
    private string Level;
    private Scene level;

    public Text StatusText;

    void Awake()
    {
        manager = FindObjectOfType<NetworkManager>();
    }
    void Start()
    {
        //StartCoroutine(ChangeScene());
        if(HostMenu.SceneName != null)
        {
            Level = HostMenu.SceneName;
        }
        else
        {
            manager.onlineScene = Level;
        }
    }
    /*void Update()
    {
        Debug.Log(ClientConnected);
        if (isServer)
        {
            if(ClientConnected == true)
            {
                Debug.Log("change scene");
                manager.ServerChangeScene(HostMenu.SceneName);
            }
        }
    }*/
    /*IEnumerator ChangeScene()
    {
        if (WaitingTime == 0)
        {
            //all clients have to say now when they are ready to change to the next scene
            NetworkServer.SetAllClientsNotReady();
            if (HostMenu.SceneName != null)
            {
                //waits until the host and client are in synch for the loading of the next scene.
                yield return SceneManager.LoadSceneAsync(Level, LoadSceneMode.Additive);
                //changes scene
                manager.ServerChangeScene(Level);
            }
            else
            {
                //waits until the host and client are in synch for the loading of the next scene.
                yield return SceneManager.LoadSceneAsync(Level, LoadSceneMode.Additive);
                //agreeing on changing the scene
                ClientScene.Ready(manager.client.connection);
            }
        }
        if (WaitingTime != 0 && isServer == true)
        {
            StatusText.text = WaitingTime + " seconds before the game starts.";
            WaitingTime--;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(ChangeScene());
    }*/
}
