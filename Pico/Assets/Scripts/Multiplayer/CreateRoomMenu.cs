using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI _roomName;

    RoomCanvases _roomCanvases;
    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }


    public void OnClick_CreateRoom()
    {
        if(!PhotonNetwork.IsConnected)
            return; 
        RoomOptions options = new RoomOptions();
        options.BroadcastPropsChangeToAll = true;
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room Successfully", this);
        _roomCanvases.CurrentRoomCanvas.Show();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Creation Failed " + message, this);
    }
}