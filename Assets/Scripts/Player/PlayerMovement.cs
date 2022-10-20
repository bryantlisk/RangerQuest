using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2D characterController2D;
    [SerializeField] float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch;
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;

        if (Input.GetButtonDown("Jump") == true)
            jump = true;
        if (Input.GetButtonDown("Crouch") == true)
            crouch = true;
        else if (Input.GetButtonUp("Crouch") == true)
            crouch = false;
    }

    void FixedUpdate()
    {
        characterController2D.Move(horizontalMove*Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
