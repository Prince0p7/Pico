using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompletedLevel : MonoBehaviour
{
    PlayerPlatformerController controller;
    Player player;
    private void Awake()
    {
        controller = GetComponent<PlayerPlatformerController>();
        player = GetComponent<Player>();
    }
    public void _PlayerCompletedLevel()
    {
        if (!controller.hasPlayerCompleted)
        {
            player.enabled = false;
            controller.spriteRenderer.enabled = false;
            controller.hasPlayerCompleted = true;
        }
        else
        {
            controller.hasPlayerCompleted = false;
            player.enabled = true;
            controller.spriteRenderer.enabled = true;
        }
    }
}