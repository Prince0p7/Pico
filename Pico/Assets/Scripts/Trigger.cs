using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public List<Transform> Players;
    public bool DoorEnter;
    [SerializeField] GameObject movement;
    [SerializeField] CameraFollow cameraFollow;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Movement>().DoorEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Movement>().DoorEnter = false;
        }
    }
}