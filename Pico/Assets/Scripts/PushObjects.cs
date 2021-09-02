using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjects : MonoBehaviour
{
    float force = 15;
    Rigidbody2D rb;
    bool canPush;
    Vector2 dir;
    [SerializeField] Collider2D TopCol;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if(canPush)
        {
            rb.velocity = new Vector2(dir.x , 0) * force;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if(!collision.collider.IsTouching(TopCol))
            {
                dir = collision.contacts[0].point - (Vector2)collision.collider.transform.position;
                dir = dir.normalized;
                canPush = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            canPush = false;
        }
    }
}