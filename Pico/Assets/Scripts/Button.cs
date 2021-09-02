using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    SpriteRenderer sprite;
    public bool Pressed;
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
    }
    void ButtonPress()
    {
        sprite.color = Color.green;
        Pressed = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ButtonPress();
        }
    }
}