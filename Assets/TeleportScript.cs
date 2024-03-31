using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform teleportTarget;
    public Transform cameraTransform;

    public string loc;
    public Vector3 targetPosition;

    public GameObject whoop;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameObject.Find(loc) == null)
        {
            PlayerScript playerScript = collision.gameObject.GetComponent<PlayerScript>();

            playerScript.room++;

            if (cameraTransform != null)
            {
                cameraTransform.position = cameraTransform.position-collision.transform.position+targetPosition;
                cameraTransform.rotation = teleportTarget.rotation;
            }
            collision.transform.position = targetPosition;
            Instantiate(whoop, transform.position, Quaternion.identity);
        }
    }
}