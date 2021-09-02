using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    void SetCustomNo()
    {
        System.Random rnd = new System.Random();
        int result = rnd.Next(0, 90);

        _text.text = result.ToString();

        _myCustomProperties["RandomNumber"] = result;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
    }
    public void OnClick_Button()
    {
        SetCustomNo();
    }
}