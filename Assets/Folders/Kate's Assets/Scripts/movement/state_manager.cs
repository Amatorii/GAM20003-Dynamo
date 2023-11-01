using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class state_manager : MonoBehaviour
{
#region private variables

    player_move stateCurrent;
    // current movement state

    CharacterController body;

    #endregion

#region debug variables

    [SerializeField] private string stateName;
    // the currently active state

    [SerializeField] private float magnitude;
    // scalar velocity

    #endregion

#region public variables

    public bool dummy;
    // if enabled, no input

    public Vector3 velocity;

    #endregion

    #region functions

    public void DetectState() // checks what state the player should be in. returns true if the state has changed
    {
        player_move stateNext = stateCurrent.CheckState();
        // calling the movement state. if nothing is returned, the state did not transition. if something was returned, the player has not moved

        if (stateNext != null)
        {
            Debug.Log("[" + name + "] Movement: Changing movement state from " + stateCurrent.name + " to " + stateNext.name + ".");

            stateCurrent = stateNext;
            stateName = stateCurrent.name;
            if (!dummy) stateCurrent.CheckInput();
        }
    }

    public void Launch(Vector3 launch)
    {
        Debug.Log("[" + name + "] Movement: Changing movement state from " + stateCurrent.name + " to air.");

        if (stateCurrent.name == "rail")
            stateCurrent.railSkip();

        stateCurrent = new move_air(body, launch);
        stateName = stateCurrent.name;
    }

    #endregion

#region events

    private Animator animator;

    void Awake()
    {
        EventManager.PlayerHasDied += Death;
        body = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        stateCurrent = new move_ground(body);
        stateName = stateCurrent.name;
    }

    private void Death()
    {
        if (!animator.enabled)
        {
            animator.enabled = true;
            animator.Play("Death");
        }
    }

    void Update()
    {
        if(GetComponent<ent_health>().health <= 0)
        {
            EventManager.PlayerIsDead();
            return;
        }
        if (!dummy) stateCurrent.CheckInput();

        DetectState();
        stateCurrent.Move();
        // calling movement state

        velocity = body.velocity;
        magnitude = velocity.magnitude;
        // this is all editor debug stuff
    }

    #endregion
}
