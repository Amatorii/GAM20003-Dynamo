using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class weapon_state : MonoBehaviour
{
    public string name;
    // debug only

    public float fireRate;

    public GameObject projectile; // what is created - shotgun just has particles & damage is calculated hitscan

    public GameObject model; // weapon viewmodel

    public GameObject crosshair; // weapon crosshair

    public abstract void fire();
}
