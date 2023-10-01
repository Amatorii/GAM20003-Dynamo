using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_air : player_move
{
    int world;
    // the layermask for the world

    int railMask;
    // the layermask for rails

    public move_air(CharacterController bodyIn) // constructor - missing a bit from original, might cause problems down the line
    {
        name = "air";
        body = bodyIn;
        world = LayerMask.GetMask("World");
        railMask = LayerMask.GetMask("Rail");
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
        {
            Collider[] railIn = Physics.OverlapSphere(body.transform.position + (Vector3.down * body.height / 2) + (Vector3.up * body.radius), body.radius, railMask);
            if (railIn.Length != 0)
            {
                Debug.Log("[" + name + "] Check State: made contact with a rail.");
                return ToRail(railIn[1].gameObject.GetComponent<rail_segment>());
            }
            else
                return null;
        }
    }

#region functions

    player_move ToRail (rail_segment railIn) // for doing rail transitions - collects information to send to rail movement state in constructor
    {
        rail_system rail = railIn.transform.parent.gameObject.GetComponent<rail_system>(); // finding rail system

        float railPos = railIn.GetRailPosition(body.transform.position + (Vector3.down * body.height / 2)); // local rail position
        railPos = rail.GetLinearPosition(railPos, railIn.index); // converting to linear position

        Vector3 velocity = body.velocity;
        float speed = velocity.magnitude; // linear speed

        if (Vector3.Dot(railIn.transform.forward, velocity.normalized) > 0)
            speed *= -1; // changing direction if they are going backwards

        return new move_rail(body, rail, railPos, speed);
    }

#endregion
}
