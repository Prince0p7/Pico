using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool canEnterDoor;
    PlayerCompletedLevel player;
    public bool haveAllPlayersEnteredTheDoor;
    PlayerPlatformerController[] controller;
    void Update()
    {
        ExitDoor();
        controller = FindObjectsOfType<PlayerPlatformerController>();
    }
    void CheckPlayerCompletion()
    {
        haveAllPlayersEnteredTheDoor = AreAllPlayersInside();
    }
    private bool AreAllPlayersInside()
    {
        for (int i = 0; i < controller.Length; i++)
        {
            if (controller[i].hasPlayerCompleted == false)
            {
                Debug.Log("Not All Players Joined");
                return false;
            }
        }

        Debug.Log("All Players Joined");
        return true;
    }

    #region ExitRound
    void ExitDoor()
    {
        if (canEnterDoor)
        {
            GoingInsideDoor();
        }
    }
    void GoingInsideDoor()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player._PlayerCompletedLevel();
            CheckPlayerCompletion();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canEnterDoor = true;
            player = collision.transform.GetComponent<PlayerCompletedLevel>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canEnterDoor = false;
            player = null;
        }
    }
    #endregion
}