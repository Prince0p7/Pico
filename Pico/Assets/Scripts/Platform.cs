using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    float Direction;
    [SerializeField] bool DownDirection, CanMove;
    public float speed;
    public int PlayerOnPlatform;
    void LateUpdate()
    {
        _Platform();
    }
    void _Platform()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DownDirection = !DownDirection;
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
        Player p = collision.collider.GetComponent<Player>();
        if (p != null)
        {

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Player p = collision.collider.GetComponent<Player>();
        if (p != null)
        {

        }
    }
}