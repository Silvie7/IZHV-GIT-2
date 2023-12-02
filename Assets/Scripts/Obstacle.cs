using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// The main Obstacle behaviour.
/// </summary>
public class Obstacle : MonoBehaviour
{
    /// <summary>
    /// Mask for objects causing the obstacle to disappear.
    /// </summary>
    public LayerMask despawnLayerMask;

    /// <summary>
    /// Movement speed of this obstacle.
    /// </summary>
    public float movementSpeed = 1.0f;
    private float movementSpeedSnapshot = 0;

    /// <summary>
    /// Direction of movement.
    /// </summary>
    public float2 movementDirection = new float2(-1.0f, 0.0f);

    /// <summary>
    /// Our RigidBody used for physics simulation.
    /// </summary>
    private Rigidbody2D mRB;

    /// <summary>
    /// Our BoxCollider used for collision detection.
    /// </summary>
    private BoxCollider2D mBC;

    private Spawner spawner;
    private GameManager gameManager;


    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    void Start()
    {
        mRB = GetComponent<Rigidbody2D>();
        mBC = GetComponent<BoxCollider2D>();



        spawner = GameObject.FindGameObjectWithTag("spawner").GetComponent<Spawner>();
        gameManager = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>();

        movementSpeedSnapshot = movementSpeed;
    }

    /// <summary>
    /// Update called once per frame.
    /// </summary>
    void Update()
    {
        if (gameManager != null)
        {
            if (!gameManager.mGameLost && !gameManager.mGameWin)
            {
                if (spawner != null)
                {
                    movementSpeed = movementSpeedSnapshot + spawner.addedMovementSpeed;
                }
                //mRB.velocity = movementDirection * movementSpeed;
                transform.position -= new Vector3(((movementSpeed) * 2)*Time.deltaTime, 0, 0);
            }
        }
    }

    /// <summary>
    /// Event triggered when we collide with something.
    /// </summary>
    /// <param name="other">The thing we collided with.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check the collided object against the layer mask.
        var hitDespawn = mBC.IsTouchingLayers(despawnLayerMask);
        
        // If we collide with any de-spawner -> destroy this object.
        if (hitDespawn)
        { Destroy(gameObject); }
    }
}
