using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public int speed;
    public Vector2 direction;
    public bool friendly;

    private Rigidbody2D rb;

    public Material material;

    public GameObject die;
    public GameObject ow;
    public GameObject pew;

    public GameObject robot1;
    public GameObject robot2;
    public GameObject robot3;

    Vector2 startPoint;

    private void Start()
    {
        startPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        Instantiate(pew, transform.position, Quaternion.identity);

    }

    private void Update()
    {
        rb.velocity = direction.normalized * speed;
        if (Vector2.Distance(startPoint, transform.position) > 6)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Robot1") || collision.gameObject.CompareTag("Robot2") || collision.gameObject.CompareTag("Robot3")) && friendly)
        {
            SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();

            spriteRenderer.material = material;

            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();

            enemyScript.Hurt(damage);

            if (enemyScript.health <= 0)
            {
                Instantiate(die, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(ow, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boss") && friendly)
        {
            SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();

            spriteRenderer.material = material;

            BossScript enemyScript = collision.gameObject.GetComponent<BossScript>();

            enemyScript.Hurt(damage);

            if (enemyScript.health <= 0)
            {
                Instantiate(die, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(ow, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!friendly)
            {
                PlayerScript playerScript = collision.gameObject.GetComponent<PlayerScript>();

                playerScript.ghost = true;

                playerScript.hurtTick = 0.5f;

                playerScript.hurtDirection = direction*15;

                if (playerScript.room <= 3)
                {
                    GameObject parent = GameObject.Find("Enemies" + playerScript.room);
                    if (playerScript.robot == 1)
                    {
                        Instantiate(robot1, collision.transform.position, Quaternion.identity, parent.transform);
                    }
                    if (playerScript.robot == 2)
                    {
                        Instantiate(robot2, collision.transform.position, Quaternion.identity, parent.transform);
                    }
                    if (playerScript.robot == 3)
                    {
                        Instantiate(robot3, collision.transform.position, Quaternion.identity, parent.transform);
                    }
                }
                else
                {
                    if (playerScript.robot == 1)
                    {
                        Instantiate(robot1, collision.transform.position, Quaternion.identity);
                    }
                    if (playerScript.robot == 2)
                    {
                        Instantiate(robot2, collision.transform.position, Quaternion.identity);
                    }
                    if (playerScript.robot == 3)
                    {
                        Instantiate(robot3, collision.transform.position, Quaternion.identity);
                    }
                }

                if (playerScript.robot != 0)
                {
                    Instantiate(die, transform.position, Quaternion.identity);
                    playerScript.robot = 0;
                }
                else
                {
                    Instantiate(ow, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (Vector2.Distance(startPoint, transform.position) > 0.8)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Destructable"))
        {
            Instantiate(ow, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}