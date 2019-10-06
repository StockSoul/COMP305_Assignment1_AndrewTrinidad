using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BackgroundController
//Andrew Trinidad
//Last Modified: Oct 5, 2019
//Program Description: This controller allows the
//background to scroll down towards the reset point
//and resetting it to the reset position

public class BackgroundController : MonoBehaviour
{
    public float verticalSpeed;
    public float resetPosition;
    public float resetPoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    /// <summary>
    /// Moves Background down by verticalSpeed
    /// </summary>
    void Move()
    {
        Vector2 newPosition = new Vector2(0.0f, verticalSpeed);
        Vector2 currentPosition = transform.position;

        currentPosition -= newPosition;
        transform.position = currentPosition;
    }

    /// <summary>
    /// Resets Background to the resetPosition
    /// </summary>
    void Reset()
    {
        transform.position = new Vector2(0.0f, resetPosition);
    }

    /// <summary>
    /// Checks if Background reaches the lower boundary
    /// and then it Resets it
    /// </summary>
    void CheckBounds()
    {
        if (transform.position.y <= resetPoint)
        {
            Reset();
        }
    }
}
