using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform _content;
    [SerializeField] RoomListings _roomListings;

    List<RoomListings> _listings = new List<RoomListings>();
    RoomCanvases roomCanvases;

    public void FirstInitialize(RoomCanvases canvases)
    {
        roomCanvases = canvases;
    }


    public override void OnJoinedRoom()
    {
        roomCanvases.CurrentRoomCanvas.Show();
        _content.DestroyChildren();
        _listings.Clear();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x._roomInfo.Name == info.Name);
                if(index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else
            {
                int index = _listings.FindIndex(x => x._roomInfo.Name == info.Name);
                if (index == -1)
                {
                    RoomListings listings = Instantiate(_roomListings, _content);
                    if (listings != null)
                    {
                        listings.SetRoomInfo(info);
                        _listings.Add(listings);
                    }
                }
            }
        }
    }
}