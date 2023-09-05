using Hamish.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airborne : Movement
{
    public Airborne(PlayerController pController) : base(pController)
    {
    }

    public override void CheckInput()
    {
        _input = new FrameInput
        {
            X = Input.GetAxisRaw("Horizontal"),
            Y = Input.GetAxisRaw("Vertical"),
            mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity * 40f,
            mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity * 40f
        };
    }

    public override Movement ExecuteMovement()
    {
        CheckInput();
        _pController.SetMovementDirection(_input.X, _input.Y);
        _pController.ControlCamera(_input.mouseY, _input.mouseX);
        if(_pController.grounded)
        {
            return new Walking(_pController);
        }
        return this;
    }
}
