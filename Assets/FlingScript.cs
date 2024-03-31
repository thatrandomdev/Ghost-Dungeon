using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlingScript : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 5f;
    public float deceleration = 2f;

    private bool isMoving = true;

    void Update()
    {
        if (isMoving)
        {
            // Calculate the movement vector based on direction and speed
            Vector3 movement = new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime;

            // Move the object
            transform.Translate(movement);

            // Gradually decrease the speed
            speed -= deceleration * Time.deltaTime;

            // Check if speed has reached or gone below zero
            if (speed <= 0f)
            {
                speed = 0f;
                isMoving = false;
            }
        }
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}