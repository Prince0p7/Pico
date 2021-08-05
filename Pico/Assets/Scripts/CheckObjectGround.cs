using System.Collections.Generic;
using UnityEngine;

public class CheckObjectGround : MonoBehaviour
{
    public float Range, Drag;
    public LayerMask mask;
    public bool hitting;
    void Update()
    {
        Collider2D box = GetComponent<BoxCollider2D>();
        Debug.DrawRay(transform.position, Vector3.down * Range, Color.red);
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector3.down, Range);
        hitting = hit;
        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            GetComponent<Rigidbody2D>().drag = Drag;
        }
        else
        {
            GetComponent<Rigidbody2D>().drag = 0;
        }
    }
}