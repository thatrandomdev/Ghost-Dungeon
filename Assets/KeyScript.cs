using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public string targetObjectName;  // Name of the object to find and move towards
    public float moveSpeed = 5f;     // Speed at which the object moves towards the target

    private GameObject targetObject; // Reference to the target object

    private void Start()
    {
        if (targetObjectName == null || targetObjectName == "")
        {
            Destroy(gameObject);
        }
        else
        {
            targetObject = GameObject.Find(targetObjectName);
        }
    }

    private void Update()
    {
        if (targetObjectName == null || targetObject == null)
        {
            Destroy(gameObject);
        }
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = targetObject.transform.position - transform.position;
        direction.Normalize();

        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}