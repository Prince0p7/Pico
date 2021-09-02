using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    PhysicsObject User;
    PhotonView view;
    Laser laser;
    [SerializeField] LineRenderer lr;
    [HideInInspector] public bool Freeze;
    [HideInInspector] public float UnfreezeTime;
    void Awake()
    {
        User = GetComponent<PhysicsObject>();
        view = GetComponent<PhotonView>();
        laser = GetComponentInChildren<Laser>();
    }
    void FixedUpdate()
    {
        if(view.IsMine)
        {
            User.PlayerMovementFixedUpdate();
        }
    }
    void Update()
    {
        if(view.IsMine)
        {
            User.PlayerMovement();
            if (!Freeze)
            {
                PlayerNearby();
                User.PlayerMovementUpdate();
                laser.LaserUpdate();
            }

            if(Freeze)
            {
                laser.DisableLaser();
            }
        }
    }
    void PlayerNearby()
    {
       Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 3.5f);
        foreach(Collider2D c in col)
        {
            if (c.GetComponent<Player>() != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector2)c.transform.position - (Vector2)transform.position);
                if (hit)
                {
                    Player P = hit.collider.GetComponent<Player>();
                    if (P != null)
                    {
                        Debug.DrawRay((Vector2)transform.position, (Vector2)c.transform.position - (Vector2)transform.position);
                        lr.enabled = true;
                        lr.SetPosition(0, transform.position);
                        lr.SetPosition(1, P.transform.position);
                        if (P.Freeze)
                        {
                            Debug.Log("PLayer can be Revived");
                            if (Input.GetKey(KeyCode.G))
                            {
                                P.UnFreeze();
                            }
                            if (Input.GetKeyUp(KeyCode.G))
                            {
                                P.UnfreezeTime = 0;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Obstacles between PLayers");
                        lr.enabled = false;
                    }
                }
                else
                {
                    lr.enabled = false;
                }
            }
        }
    }
    void UnFreeze()
    {
        //Jab player freeze hoga uske baad hm usko unfreeze karaenge.
        // Sabse pehle player ke paas honge 
        // phir player ke pas ek revive type button hoga usko hold karke rakhna hoga
        // agar vo certain time tk unfreeze hold raha to player unfreeze ho jaayega or dono player ke pas 2 second ki shield aa jayegi to vo freeze nhi honge
        UnfreezeTime += Time.deltaTime;
        if (UnfreezeTime > 3)
        {
            Debug.Log("Player Unfreezed");
            Freeze = false;
            UnfreezeTime = 0;
        }
        UnfreezeTime = Mathf.Clamp(UnfreezeTime, 0, 3);
    }
}