using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] CharacterController characController;
    [SerializeField] FixedJoystick joystick;
    float Speed = 5f;
    float Gravity = 5f;
    float verticalVelocity;
    Quaternion targetRotation;
    private bool isRunning;

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Vector3 move = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        move.y = Vertical();
        characController.Move(move * Speed * Time.fixedDeltaTime);
        bool isMoving = move.x != 0 || move.z != 0;
        if (isMoving)
        {
            targetRotation = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));
            transform.parent.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * Speed);

            if (!isRunning)
            {
                ChangeAnim(Constants.ANIM_RUN);
                isRunning = true;
            }
        }
        else
        {
            if (isRunning)
            {
                ChangeAnim(Constants.ANIM_IDLE);
                isRunning = false;
            }
        }
    }
    private float Vertical()
    {
        if (characController.isGrounded) verticalVelocity = -1;
        else verticalVelocity -= Gravity * Time.fixedDeltaTime;
        return verticalVelocity;
    }
}
