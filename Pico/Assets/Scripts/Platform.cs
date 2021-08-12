using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    float Direction;
    [SerializeField] bool DownDirection, CanMove;
    public float speed;
    public List<Rigidbody2D> rbs;
    void LateUpdate()
    {
        _Platform();
    }
    void _Platform()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (DownDirection)
            {
                DownDirection = false;
            }
            else
            {
                DownDirection = true;
            }
        }
        float YPos = transform.localPosition.y;
        if (CanMove)
        {
            if (DownDirection)
            {
                if (YPos <= -1)
                {
                    Direction = 0;
                }
                else
                {
                    Direction = -1;
                }
            }
            else if (!DownDirection)
            {
                if (YPos >= 1)
                {
                    Direction = 0;
                }
                else
                {
                    Direction = 1;
                }
            }
        }
        else
        {
            Direction = 0;
        }
        transform.Translate(Vector3.up * Direction * 1/speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if (rb.gameObject.tag == "Player")
            {
                if(!rbs.Contains(rb))
                {
                    rbs.Add(rb);
                    rb.transform.parent = transform;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if (rbs.Contains(rb))
            {
                rbs.Remove(rb);
                rb.transform.parent = null;
            }
        }
    }
}