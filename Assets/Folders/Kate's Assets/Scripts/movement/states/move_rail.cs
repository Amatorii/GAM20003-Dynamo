using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_rail : player_move
{
    public rail_system rail;
    float position; // linear position on rail
    float velocity; // signed linear velocity - does not change

    float offset; //vertical offset from rail

    // variables
    float coJump = 7.5f;

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

        body.transform.position = rail.ProjectOnRail(position) + Vector3.up * offset; // setting new position
    }

    public override player_move CheckState() // used to see if the player should enter a new movement state
    {
        if (moveInput.inputJump)
        {
            Vector3 velOut = (rail.GetDirection(position) * velocity);

            if (moveInput.inputY != 0) // holding forward
                velOut = (body.transform.forward * moveInput.inputY * 10) + (Vector3.up * velOut[1]);

            velOut += (Vector3.up * coJump);
            if (velOut[1] < coJump) velOut[1] = coJump;
            // adding force to current velocity with vertical magnitude becoming at least that of coJump

            rail.StartCoroutine("Skip");
            body.enabled = true;
            return new move_air(body, velOut);
        }
        // jump

        else if ((velocity >= 0 && position >= rail.totalLength) || (velocity < 0 && position <= 0))
        {
            Vector3 velOut = (rail.GetDirection(position) * velocity);

            rail.StartCoroutine("Skip");
            body.enabled = true;
            return new move_air(body, velOut);
        }
        // end of rail

        else
            return null;
    }

    #region functions

    public void railSkip()
    {
        rail.StartCoroutine("Skip");
    }

    #endregion
}
