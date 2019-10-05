using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Variable Declaration
    [Header("Movement Settings")]
    public float speed = 5.0f;
    // public float xMin, xMax, yMin, yMax;
    public Boundary boundary;

    [Header("Attack Settings")]
    public GameObject laser;
    public GameObject laserSpawn;
    public float fireRate = 0.5f;

    private Rigidbody2D rBody;
    private float myTime = 0.0f;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        myTime += Time.deltaTime;

        if (Input.GetKeyDown("space") && myTime > fireRate)
        {
            Instantiate(laser, laserSpawn.transform.position, laserSpawn.transform.rotation);
            myTime = 0.0f;  
        }
	}

    // Fixed update increments. Used for physics!
    void FixedUpdate()
    {
        // Read input
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        //Debug.Log("x: " + horiz + ", y: " + vert);

        Vector2 movement = new Vector2(horiz, vert);

        // Move the player
        // Rigidbody2D rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = movement * speed;

        // Restrict the player from leaving the play area
        rBody.position = new Vector2(
            Mathf.Clamp(rBody.position.x, boundary.xMin, boundary.xMax),  // Restrict the x position to xMin and xMax
            Mathf.Clamp(rBody.position.y, boundary.yMin, boundary.yMax)); // Restrict the y position to yMin and yMax
    }
}
