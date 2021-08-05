using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController2D[] controller;
    public float Speed;
    public bool Crouch, Jump, DoorEnter;
    void Start()
    {

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump = true;
        }
        controller[0].Move(Input.GetAxisRaw("Horizontal") * Speed * Time.fixedDeltaTime, Crouch, Jump);
        Jump = false;
    }
}