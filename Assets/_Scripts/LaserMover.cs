using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LaserMover
//Andrew Trinidad
//Last Modified: Oct 5, 2019
//Program Description: This controller allows laser 
//to travel upwards toward the top of the screen

public class LaserMover : MonoBehaviour
{
    // Variable Declarations
    public GameObject gameObject;
    public float speed;

    private Rigidbody2D rBody;

    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();

        //I know this looks wonky professor.
        //but it's what is working.
        rBody.velocity = transform.right * speed;
    }

    //If the laser goes offscreen destroy it 
    //so it doesn't just have endless clones
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
