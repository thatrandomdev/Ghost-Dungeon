using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform target; // The object to follow
    public float followSpeed = 5f; // The speed at which the object follows the target

    void Update()
    {
        if (target != null)
        {
            // Calculate the target position slightly above the target object
            Vector3 targetPosition = target.position;

            // Move the current object towards the target position using Lerp
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }
}