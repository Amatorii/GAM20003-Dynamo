using Hamish.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement
{
    protected PlayerController _pController;
    protected FrameInput _input;
    protected float sensitivity;

    public Movement(PlayerController pController)
    {
        _pController = pController;
        sensitivity = pController.GetSensitivity();
        //Debug.Log("[MOVEMENT:" + this + "] state is active");
    }
    public abstract Movement ExecuteMovement();

    public abstract void CheckInput();

    /// <summary>
    /// Must be called in regular Update and not FixedUpdate
    /// </summary>
    protected struct FrameInput
    {
        /// <summary>
        /// X represents the player's horizontal movement (A and D)
        /// Y is foward and backwards (W and S)
        /// </summary>
        public float X, Y;
        /// <summary>
        /// This is to track the player's mouse movements. It's supper scuffed so the code might not make sense but it works so don't worry about it.
        /// </summary>
        public float mouseX, mouseY;
        /// <summary>
        /// If the player is holding down space
        /// </summary>
        public bool JumpDown;
        public bool JumpUp;

        public bool Shift;
    }
    ///TODO:
    /// - Make Controls, assignable (Currently, they're the default unity controls)
}
