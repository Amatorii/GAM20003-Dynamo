using Hamish.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimb : Movement
{
    public WallClimb(PlayerController pController) : base(pController)
    {
    }

    public override void CheckInput()
    {
        throw new System.NotImplementedException();
    }

    public override Movement ExecuteMovement()
    {
        if (true)
            return new Airborne(_pController);
        return this;
    }
}
