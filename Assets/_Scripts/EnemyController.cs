using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class EnemyController : MonoBehaviour
{
    [Header("Speed Values")]
    [SerializeField]
    public Speed horizontalSpeedRange;

    [SerializeField]
    public Speed verticalSpeedRange;

    public float verticalSpeed;
    public float horizontalSpeed;

    [SerializeField]
    public Boundary boundary;

    // Start is called before the first frame update
    void Start()
    {
        RandomMove();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //CheckBounds();
    }

    /// <summary>
    /// Moves enemy down by vertical speed
    /// </summary>
    void Move()
    {
        Vector2 newPosition = new Vector2(horizontalSpeed, verticalSpeed);
        Vector2 currentPosition = transform.position;

        currentPosition -= newPosition;
        transform.position = currentPosition;
    }

    /// <summary>
    /// Resets enemy to reset position
    /// </summary>
    void RandomMove()
    {
        horizontalSpeed = Random.Range(horizontalSpeedRange.min, horizontalSpeedRange.max);
        verticalSpeed = Random.Range(verticalSpeedRange.min, verticalSpeedRange.max);
    
        float randomXPosition = Random.Range(boundary.xMin, boundary.xMax);
        transform.position = new Vector2(randomXPosition, Random.Range(boundary.yMax, boundary.yMax + 2.0f));
    }

    /// <summary>
    //Enemy has reached the bottom of the screen and forces it to the top.
    /// </summary>
    //void CheckBounds()
    //{
    //    if (transform.position.y <= boundary.yMin)
    //    {
    //        Reset();
    //    }
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Lazer")
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
