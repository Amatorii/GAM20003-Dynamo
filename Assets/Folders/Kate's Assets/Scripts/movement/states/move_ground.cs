using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_ground : player_move
{
    int world;
    // the layermask for the world

    bool grounded;
    // remembering if the player is grounded (for a weird edge case)

    // variables
    float coJump = 7.5f;

    public move_ground(CharacterController bodyIn) // constructor - missing a bit from original, might cause problems down the line
    {
        name = "ground";
        body = bodyIn;
        world = LayerMask.GetMask("World");
    }

    public override void Move() // called every frame
    {
        Vector3 normal = GroundCheck();
        // finding the gradient of the ground

        Vector3 intention = Vector3.ClampMagnitude((body.transform.right * moveInput.inputX + body.transform.forward * moveInput.inputY), 1);
        // input mapped on to player rotation & magnitude clamped

        intention = Vector3.ProjectOnPlane(intention, normal);

        Vector3 velocity = Vector3.ProjectOnPlane(body.velocity, normal); //velocity[1] = 0;
        // converting velocity to a flattened version

        velocity = Vector3.MoveTowards(velocity, intention * 10, 80 * Time.deltaTime);
        // input applied as acceleration

        velocity = Vector3.ProjectOnPlane(velocity, normal);
        // velocity applied to the gradient

        Debug.DrawRay(body.transform.position, velocity / 5, Color.red);
        // displaying player's velocity for debug

        body.Move(velocity * Time.deltaTime);
        // velocity applied to the player

        if (body.velocity.magnitude > 0)
            grounded = body.isGrounded;
    }

    public override player_move CheckState() // used to see if the player should enter a new movement state
    {
        if (moveInput.inputJumpHold)
        {
            Vector3 velocity = body.velocity + (Vector3.up * coJump);
            if (velocity[1] < coJump) velocity[1] = coJump;
            // adding force to current velocity with vertical magnitude becoming at least 7.5

            return new move_air(body, velocity);
        }
        // jump

        else if (!grounded && !Physics.Raycast(body.transform.position, Vector3.down, (body.height * 0.5f) + 1, world)) // see groundcheck for explanation of this cast
        {
            return new move_air(body);
        }
        // fall

        else
            return null;
        // no change
    }

#region functions

    Vector3 GroundCheck() // returns the normal of the ground and snaps the player down to it
    {
        float radius = body.radius;
        // how wide the spherecast is

        float feet = (body.height * 0.5f);
        // how far the feet are from the center of the body

        if ((body.collisionFlags & CollisionFlags.Sides) == 0)
            radius -= 0.05f;
        // if the player is touching a wall, the cast needs to be a little thinner so it doesn't get caught on that

        Debug.DrawRay(body.transform.position, Vector3.down * (feet - body.radius + 1), Color.red);
        // this is the line that goes down from the player in inspector

        if 
            (
            Physics.SphereCast(body.transform.position, radius, Vector3.down, out RaycastHit hit, feet - body.radius + 1, world) // note: layer reference (currently 6: World) will not be updated if layers are changed
            && Physics.Raycast(body.transform.position, Vector3.down, out RaycastHit hitRay, feet + 1, world) // making sure it isn't just a ledge
            )
        {
            /*if (hitRay.distance > feet && hitRay.normal[1] == 1)
            {
                Debug.Log("[" + name + "] Ground Movement: Player is off the ground - snapping " + (hitRay.distance - feet) + "units down.");
                body.transform.Translate(Vector3.down * (hitRay.distance - feet));
            }*/
            // snapping the player to the ground if they are above - only applies on flat ground

            Debug.DrawRay(hit.point, hit.normal, Color.green);
            // displaying ground normal for debug

            return hit.normal;
        }

        else
        {
            //Debug.Log("no hits");
            return Vector3.up;
        }
    }

    #endregion
}
