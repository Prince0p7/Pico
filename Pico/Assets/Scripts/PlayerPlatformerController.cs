using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public string MoveInput;
    public KeyCode Jump = KeyCode.None;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    bool m_FacingRight = true;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
     //   animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis(MoveInput);

        if (Input.GetKeyDown(Jump) && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }

        if (move.x > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move.x < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        //   animator.SetBool("grounded", grounded);
        //   animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        if (spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}