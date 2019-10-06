using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Diamond Controller
//Andrew Trinidad
//Last Modified: Oct 5, 2019
//Program Description: This controller allows the
//Diamond to move down the screen.
public class DiamondController : MonoBehaviour
{
    public float verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// Moves Diamond down by verticalSpeed
    /// </summary>
    void Move()
    {
        Vector2 newPosition = new Vector2(0.0f, verticalSpeed);
        Vector2 currentPosition = transform.position;

        currentPosition -= newPosition;
        transform.position = currentPosition;
    }

    /// <summary>
    /// If Diamond goes offscreen destroy it
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// If Diamond collides with the player destroy it.
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }

    }
}
