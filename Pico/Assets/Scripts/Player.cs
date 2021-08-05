using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool DoorEnter, hasEnteredDoor;
    void Update()
    {
        Door();
    }
    void Door()
    {
        DoorEnter = transform.GetChild(0).GetComponent<Movement>().DoorEnter;
        if (DoorEnter)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                hasEnteredDoor = true;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (hasEnteredDoor)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                hasEnteredDoor = false;
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}