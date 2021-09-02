using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject Player;
    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] List<GameObject> Players;
    GameObject PL;
    void Start()
    {
        PL = PhotonNetwork.Instantiate(Player.name, SpawnPoints[0].position, Quaternion.identity);
    }
    void Update()
    {
        if (!Players.Contains(PL))
        {
            Players.Add(PL);
        }
    }
}