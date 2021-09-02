using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhysicsObject : MonoBehaviour
{
    float minGroundNormalY = .65f;
    [HideInInspector] public float gravityModifier = 10f;
    Transform parent;

    [HideInInspector] public SpriteRenderer spriteRenderer;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        parent = null;
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }
    public void PlayerMovementUpdate()
    {
        FlipSprite();
        ComputeVelocity();
    }
    public void PlayerMovement()
    {
        PlatformChecker();
        transform.SetParent(parent);
        targetVelocity = Vector2.zero;
    }
    void PlatformChecker()
    {
        Debug.DrawRay(transform.position, Vector3.down * spriteRenderer.bounds.extents.y * 3, Color.blue);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3.up * spriteRenderer.bounds.extents.y), spriteRenderer.bounds.size, 0, Vector3.down, spriteRenderer.bounds.extents.y * 3);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Platform")
            {
                parent = hit.collider.transform;
            }
            else
            {
                parent = null;
            }
        }
        else if (!grounded)
        {
            parent = null;
        }
    }
    protected virtual void FlipSprite()
    {

    }
    protected virtual void ComputeVelocity()
    {

    }

    public void PlayerMovementFixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }
    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                if (hitBuffer[i].transform.GetComponent<PlayerPlatformerController>() != null)
                {
                    if(!hitBuffer[i].transform.GetComponent<PlayerPlatformerController>().hasPlayerCompleted)
                    {
                        hitBufferList.Add(hitBuffer[i]);
                    }
                }
                else
                {
                    hitBufferList.Add(hitBuffer[i]);
                }
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        Vector3 _position = move.normalized * distance;
        rb2d.position += (Vector2)_position;
    }
}