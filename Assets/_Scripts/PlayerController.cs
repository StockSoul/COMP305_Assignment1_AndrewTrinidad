using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player Controller
//Andrew Trinidad
//Last Modified: Oct 5, 2019
//Program Description: This controller allows the player to move,
//lose lives, and gain points while restricting player to a specific space.

public class PlayerController : MonoBehaviour {

    // Variable Declaration
    [Header("Movement Settings")]
    public float speed = 5.0f;
    public Boundary boundary;

    public GameController gameController;

    private AudioSource _explodeSound;
    private AudioSource _coinSound;

    //Shooting settings
    [Header("Attack Settings")]
    public GameObject laser;
    public GameObject laserSpawn;
    public float fireRate = 0.5f;

    private Rigidbody2D rBody;
    private float myTime = 0.0f;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();

        _explodeSound = gameController.audioSources[(int)SoundClip.EXPLODE];
        _coinSound = gameController.audioSources[(int)SoundClip.COIN];
    }

    // Update is called once per frame
    void Update() {

        myTime += Time.deltaTime;

        //prevents player from just spamming laser
        if (Input.GetKeyDown("space") && myTime > fireRate)
        {
            Instantiate(laser, laserSpawn.transform.position, laserSpawn.transform.rotation);
            myTime = 0.0f;  
        }
	}
    
    void FixedUpdate()
    {
        //Read player input
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horiz, vert);

        //Move the player
        rBody.velocity = movement * speed;

        //Restrict the player from leaving the play area
        rBody.position = new Vector2(
            Mathf.Clamp(rBody.position.x, boundary.xMin, boundary.xMax),  // Restrict the x position to xMin and xMax
            Mathf.Clamp(rBody.position.y, boundary.yMin, boundary.yMax)); // Restrict the y position to yMin and yMax
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            //If player collides with a Enemy lose a life 
            //and play an explosion sound
            case "Enemy":
                _explodeSound.Play();
                gameController.Lives -= 1;
                break;

            //If player collides with a Diamond gain ten points
            //and play a coin sound
            case "Diamond":
                _coinSound.Play();
                gameController.Score += 10;
                break;
        }   

    }
}
