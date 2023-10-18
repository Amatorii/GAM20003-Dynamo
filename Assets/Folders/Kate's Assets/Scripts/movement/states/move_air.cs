using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_air : player_move
{
    int world;
    // the layermask for the world

    int railMask;
    // the layermask for rails

    // variables
    float railMin = 10; // minimum speed for on rail

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
        railMask = LayerMask.GetMask("Rail");

        Debug.Log("[" + name + "] Movement: Launched with a velocity of " + (launch) + ".");
        body.Move(launch * Time.deltaTime);
        // cheeky little jump before we get into the full movement stuff
    }

    public override void Move() // called every frame
    {
        Vector3 velocity = body.velocity;
        
        float grav = Mathf.MoveTowards(velocity[1], -30, 20 * Time.deltaTime);
        if (grav > 15) grav = 15; // vertical speed clamp

        velocity[1] = 0;

        Vector3 intention = Vector3.ClampMagnitude((body.transform.right * moveInput.inputX + body.transform.forward * moveInput.inputY), 1);

        if (Vector3.Dot(intention, velocity) < 0 || Vector3.Project(intention * 10, velocity).magnitude <= 1)
            velocity += intention * Mathf.Clamp01(100 * Time.deltaTime);

        velocity[1] = grav;

        body.Move(velocity * Time.deltaTime);
        // velocity applied to the player
    }

    public override player_move CheckState() // used to see if the player should enter a new movement state
    {
        if (body.isGrounded && body.velocity[1] <= 6)
            return new move_ground(body);

        else if (Vector3.ProjectOnPlane(body.velocity, Vector3.up).magnitude != 0)
        {
            Debug.DrawRay(body.transform.position + (Vector3.down * body.height / 2), Vector3.forward, Color.red);
            Collider[] railIn = Physics.OverlapSphere(body.transform.position + (Vector3.down * body.height / 2) + (Vector3.up * body.radius), body.radius, railMask);
            if (railIn.Length != 0)
            {
                Debug.Log("[" + name + "] Check State: Made contact with a rail. " + railIn.Length + " contacts total.");
                return ToRail(railIn[0].gameObject.GetComponent<rail_segment>());
            }
            else
                return null;
        }
        else
            return null;
    }

#region functions

    player_move ToRail (rail_segment railIn) // for doing rail transitions - collects information to send to rail movement state in constructor
    {
        rail_system rail = railIn.transform.parent.gameObject.GetComponent<rail_system>(); // finding rail system

        float railPos = railIn.GetRailPosition(body.transform.position + (Vector3.down * body.height / 2)); // local rail position
        railPos = rail.GetLinearPosition(railPos, railIn.index); // converting to linear position

        Vector3 velocity = body.velocity;
        velocity[1] = 0;
        
        if (velocity.magnitude < railMin)
            velocity = velocity.normalized * railMin;

        float speed = velocity.magnitude; // linear speed

        if (Vector3.Dot(railIn.transform.forward, velocity.normalized) < 0)
            speed *= -1; // changing direction if they are going backwards

        return new move_rail(body, rail, railPos, speed);
    }

#endregion
}
