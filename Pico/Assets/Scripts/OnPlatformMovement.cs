using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlatformMovement : MonoBehaviour
{
    SpriteRenderer sprite;
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        CheckPlayerUnderiT();
    }
    void CheckPlayerUnderiT()
    {
        RaycastHit2D hit = Physics2D.BoxCast(sprite.bounds.center + (Vector3.up * sprite.bounds.extents.y), sprite.bounds.size, 0, Vector2.down, sprite.bounds.extents.y);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject != gameObject && hit.collider.gameObject.tag == "Player")
            {
                transform.SetParent(hit.transform);
                Debug.Log(hit.collider.name);
            }
        }
        else
        {
            transform.SetParent(null);
        }
    }
}