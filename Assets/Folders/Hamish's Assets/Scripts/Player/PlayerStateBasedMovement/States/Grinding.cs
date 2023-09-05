using Hamish.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinding : Movement
{
    public Grinding(PlayerController pController) : base(pController)
    {

    }

    public override void CheckInput()
    {
        _input = new FrameInput()
        {
            JumpDown = Input.GetButtonDown("Jump"),
            mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity * 40f,
            mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity * 40f
        };
    }

    public override Movement ExecuteMovement()
    {
        CheckInput();
        _pController.ControlCamera(_input.mouseY, _input.mouseX);

        return this;
    }
}
