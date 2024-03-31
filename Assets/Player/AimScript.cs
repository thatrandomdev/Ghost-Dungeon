using UnityEngine;

public class AimScript : MonoBehaviour
{
    public float distance = 2f;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = mousePosition - transform.parent.position;
        direction = direction.normalized;

        Vector3 targetPosition = transform.parent.position + (direction * distance) + (Vector3.up * -0.15f);

        PlayerScript playerScript = GetComponentInParent<PlayerScript>();

        if (playerScript.robot == 0)
        {
            transform.position = Vector2.right * 100000;
        }
        else
        {
            transform.position = targetPosition;
        }
    }
}