using UnityEngine;
using UnityEngine.Audio;

public class MusicMixer : MonoBehaviour {

    public AudioMixer masterMixer;

    public void SetMasterLvl(float MasterLvl)
    {
        masterMixer.SetFloat("MasterVol", MasterLvl);
    }
}
