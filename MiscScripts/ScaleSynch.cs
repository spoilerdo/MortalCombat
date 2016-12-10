using UnityEngine;
using UnityEngine.Networking;
using System;

public class ScaleSynch : NetworkBehaviour {

    [SyncVar]
    private Vector3 SynchScale; //Dit word gesychroniseerd

    public Transform PlayerScale; //Scale van de localplayer en niet localplayer

    void FixedUpdate()
    {
        TransmitScale();
        SetScale();
    }
    void SetScale()
    {
        if(!isLocalPlayer) //ben je niet de local player dan moet jouw scale worden gesycnch
        {
            PlayerScale.localScale = SynchScale;
        }
    }
    [Command] //deze lijn code word gestuurd naar de server
    void CmdScale(Vector3 scale)
    {
        SynchScale = scale;
    }
    [Client] //Hier word CmdScale() geroepen via de client
    void TransmitScale()
    {
        if(isLocalPlayer) //je kan alleen CmdScale roepen als je de local player bent
        {
            CmdScale(PlayerScale.localScale);
        }
    }
}
