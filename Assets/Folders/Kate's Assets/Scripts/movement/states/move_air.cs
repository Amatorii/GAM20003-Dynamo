using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_air : player_move
{
    int world;
    // the layermask for the world

    public move_air(CharacterController bodyIn) // constructor - missing a bit from original, might cause problems down the line
    {
        name = "air";
        body = bodyIn;
        world = LayerMask.GetMask("World");
    }

    public move_air(CharacterController bodyIn, Vector3 launch) // secondary constructor for when a force needs to be applied
    {
        name = "air";
        body = bodyIn;
        world = LayerMask.GetMask("World");

        Debug.Log("[" + name + "] Movement: Launched with a velocity of " + (launch) + ".");
        body.Move(launch * Time.deltaTime);
        // cheeky little jump before we get into the full movement stuff
    }

    public override void Move() // called every frame
    {
        Vector3 velocity = body.velocity;

        velocity[1] = Mathf.MoveTowards(velocity[1], -30, 20 * Time.deltaTime);

        body.Move(velocity * Time.deltaTime);
        // velocity applied to the player
    }

    public override player_move CheckState() // used to see if the player should enter a new movement state
    {
        if (body.isGrounded && body.velocity[1] <= 0)
            return new move_ground(body);

        else
            return null;
    }

#region functions

    #endregion
}
