using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPlatformerController : PhysicsObject, IPunObservable
{
    PlayerReferance[] OtherPlayerComponents;
    PhotonView view;
    [HideInInspector] public float maxSpeed = 20;
    [HideInInspector] public float jumpTakeOffSpeed = 30;
    public string MoveInput;
    public KeyCode Jump = KeyCode.None;
    private Animator animator;
    bool m_FacingRight = true;
    Vector2 move = Vector2.zero;
    void Awake()
    {
        OtherPlayerComponents = GetComponentsInChildren<PlayerReferance>();
        view = GetComponent<PhotonView>();
        //   animator = GetComponent<Animator>();
        if (!view.IsMine)
        {
            foreach (PlayerReferance P in OtherPlayerComponents)
            {
                P.gameObject.SetActive(false);
            }
        }
    }

    private void AddObservable()
    {
        if (!view.ObservedComponents.Contains(this))
        {
            view.ObservedComponents.Add(this);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(spriteRenderer.flipX);
        }
        else
        {
            spriteRenderer.flipX = (bool)stream.ReceiveNext();
        }
    }

    protected override void ComputeVelocity()
    {
        move.x = Input.GetAxis(MoveInput);
        if (Input.GetKeyDown(Jump) && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }

        //   animator.SetBool("grounded", grounded);
        //   animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
    protected override void FlipSprite()
    {
        if (move.x > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move.x < 0 && m_FacingRight)
        {
            Flip();
        }
    }

     public bool hasPlayerCompleted;// is Player inside the door?
    void Flip()
    {
        m_FacingRight = !m_FacingRight;

        if (!hasPlayerCompleted)
        {
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
}