using UnityEngine;

public class DrawOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool rotationLocked = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Convert Y position to Z position
        float zPosition = (transform.position.y - (spriteRenderer.bounds.size.y / 2) * 0.8f) * 0.05f;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);

        if (rotationLocked)
        {
            // Lock the rotation to zero
            transform.rotation = Quaternion.identity;
        }
    }
}