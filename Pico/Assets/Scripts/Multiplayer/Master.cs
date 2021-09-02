using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Master : MonoBehaviour
{

    public GameObject _ReadyButton;
    public GameObject _StartButton;
    public void OnClick_CheckMaster()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _ReadyButton.SetActive(false);
            _StartButton.SetActive(true);
        }
        else
        {
            _ReadyButton.SetActive(true);
            _StartButton.SetActive(false);
        }
    }
}
