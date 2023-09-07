using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class player_move
{
    protected FrameInput moveInput;

    protected CharacterController body;

    public string name;

    protected struct FrameInput
    {
        public float inputX, inputY;

        public bool inputJump;
        public bool inputJumpHold; // if jump is being held down

        public bool inputShift;
    }

    public void CheckInput()
    {
        moveInput = new FrameInput()
        {
            inputJump = Input.GetButtonDown("Jump"),
            inputJumpHold = Input.GetButton("Jump"),
            inputShift = Input.GetKey(KeyCode.LeftShift),
            inputX = Input.GetAxisRaw("Horizontal"),
            inputY = Input.GetAxisRaw("Vertical")
        };
    }

    public abstract void Move(); // called every frame, returns velocity

    public abstract player_move CheckState(); // used to see if the player should enter a new movement state
}
