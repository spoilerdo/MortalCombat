using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Animator OptionsAnimator;
    public Animator PlayAnimator;

    public GameObject LeftHanded;
    public GameObject RightHanded;

    public Button leftButton;
    public Button rightButton;

    void Awake()
    {
        rightButton.interactable = false;
    }
    public void PlayButton()
    {
        PlayAnimator.SetBool("Pressed", true);
    }
    public void OptionsButton()
    {
        OptionsAnimator.SetBool("Pressed", true);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void BackButton()
    {
        OptionsAnimator.SetBool("Pressed", false);
        PlayAnimator.SetBool("Pressed", false);
    }
    public void LeftButton()
    {
        LeftHanded.SetActive(true);
        RightHanded.SetActive(false);
        leftButton.interactable = false;
        rightButton.interactable = true;
    }
    public void RightButton()
    {
        LeftHanded.SetActive(false);
        RightHanded.SetActive(true);
        rightButton.interactable = false;
        leftButton.interactable = true;
    }
    public void HostButton()
    {
        SceneManager.LoadScene("HostMenu");
    }
    public void JoinButton()
    {
        SceneManager.LoadScene("JoinMenu");
    }
}
