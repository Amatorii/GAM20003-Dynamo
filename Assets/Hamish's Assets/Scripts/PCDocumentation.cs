using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamish.player.draft
{
    public abstract class PCDocumentation: MonoBehaviour
    {
        /// <summary>
        /// Struct that catalogs all the keys the player can press
        /// </summary>
        public struct FrameInput
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

        /// <summary>
        /// This is here so the player controller script is tidier but also allows me to provide definitions to variables and methods
        /// </summary>
        public interface IPlayerController
        {
            /// <summary>
            /// This represents the direction the player is facing. Can be any empty game object that matches the player's rotation
            /// </summary>
            public Transform orientation { get; }

            /// <summary>
            /// Checks if the player is grounded
            /// </summary>
            public bool grounded { get; }
        }

        /// <summary>
        /// Gets the player's input from their keyboard. Not currently customizable ATM
        /// </summary>
        public abstract void GatherInput();
    }

}

