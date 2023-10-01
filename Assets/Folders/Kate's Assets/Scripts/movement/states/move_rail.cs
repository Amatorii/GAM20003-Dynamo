using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_rail : player_move
{
    rail_system rail;
    float position; // linear position on rail
    float velocity; // signed linear velocity - does not change

    float offset; //vertical offset from rail

    public move_rail(CharacterController bodyIn, rail_system railIn, float posIn, float velIn) // constructor - takes the charcontroller, current rail system, linear position on rail, and signed speed
    {
        name = "rail";
        body = bodyIn;

        rail = railIn;
        position = posIn;
        velocity = velIn;

        offset = body.height / 2;

        body.enabled = false; // disabling collisions - make sure to enable once we are done grinding
    }

    public override void Move() // called every frame
    {
        position = Mathf.Clamp(position + (velocity * Time.deltaTime), 0, rail.totalLength); // changing position

        body.transform.position = rail.ProjectOnRail(position); // setting new position
    }

    public override player_move CheckState() // used to see if the player should enter a new movement state
    {
        return null;
    }

    #region functions

    #endregion
}
