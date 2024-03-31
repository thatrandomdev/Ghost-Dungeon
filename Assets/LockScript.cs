using UnityEngine;

public class LockScript : MonoBehaviour
{
    public string enemies;

    public GameObject key;

    private GameObject player;

    public GameObject ow;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Instantiate(ow, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}