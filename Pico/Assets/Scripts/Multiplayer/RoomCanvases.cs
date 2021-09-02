using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCanvases : MonoBehaviour
{
    [SerializeField] CreateOrJoinRoomCanvas _createOrJoinRoomsCanvas;
    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas { get { return _createOrJoinRoomsCanvas; } }

    [SerializeField] CurrentRoomCanvas _currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas { get { return _currentRoomCanvas; } }

    private void Awake()
    {
        FirstInitialized();
    }
    private void FirstInitialized()
    {
        CurrentRoomCanvas.FirstInitialize(this);
        CreateOrJoinRoomCanvas.FirstInitialize(this);
    }
}