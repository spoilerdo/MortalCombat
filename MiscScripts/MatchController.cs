using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MatchController : NetworkBehaviour
{

    public Text InfoText;

    [HideInInspector]
    public static bool GameStarts = false;

    [SyncVar]
    public int MaxTimeBeforeStart = 10;

    private float WaitBeforeWinningText = 1;
    public Animator DisconnectPanel;
    private NetworkManager manager;

    void Start()
    {
        manager = FindObjectOfType<NetworkManager>();
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        if (MaxTimeBeforeStart != 0)
        {
            InfoText.text = MaxTimeBeforeStart.ToString();
            yield return new WaitForSeconds(1);
            MaxTimeBeforeStart--;
            StartCoroutine(CountDown());
        }
        else
        {
            InfoText.text = "FIGHT";
            yield return new WaitForSeconds(1);
            InfoText.text = "";
            GameStarts = true;
        }
    }
    public IEnumerator WinningController (string _Player)
    {
        yield return new WaitForSeconds(WaitBeforeWinningText);
        InfoText.text = _Player + " wins the game!";
        DisconnectPanel.SetBool("Pressed", true);
    }
    public void Disconnect()
    {
        manager.StopHost();
    }
}
