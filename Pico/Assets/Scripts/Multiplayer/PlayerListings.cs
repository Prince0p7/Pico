using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerListings : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI _text;
    public Photon.Realtime.Player Player { get; private set; }

    public bool Ready = false;

    public void SetPlayerInfo(Photon.Realtime.Player player)
    {
        Player = player;
        SetPlayerText(player);
    }
    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if(targetPlayer != null && targetPlayer == Player)
        {
            if(changedProps.ContainsKey("RandomNumber"))
            {
                SetPlayerText(targetPlayer);
            }
        }
    }
    void SetPlayerText(Photon.Realtime.Player player)
    {
        int result = -1;
        if (player.CustomProperties.ContainsKey("RandomNumber"))
        {
            result = (int)player.CustomProperties["RandomNumber"];
        }
        _text.text = result.ToString() + " , " + player.NickName;
    }
}