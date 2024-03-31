using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject bullet;
    private GameObject bullet2;
    private GameObject player;
    private PlayerScript playerScript;
    public bool active = false;
    public float speed;
    public float hurtTimer;
    private SpriteRenderer spriteRenderer;
    public Material normal;
    public Material flash;
    public int health;
    public Rigidbody2D rb;
    public float attackSpeed;

    public GameObject key;

    public float possessCooldown = 0.5f;

    float attackTime = 1.5f;
    
    Animator animator;

    string currentState;

    int direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.forward * 0);

        if (possessCooldown > 0)
        {
            possessCooldown -= Time.deltaTime;
        }

        if (hurtTimer > 0)
        {
            hurtTimer -= Time.deltaTime;
        }
        else
        {
            spriteRenderer.material = normal;
        }
        if (health <= 0)
        {
            Transform parentObject = transform.parent;
            int childCount = 2;

            if (parentObject != null)
            {
                childCount = parentObject.childCount;
            }

            if (childCount == 1)
            {
                GameObject keyObject = Instantiate(key, transform.position, Quaternion.identity);
                KeyScript keyScript = keyObject.GetComponent<KeyScript>();
                if (parentObject.name == "Enemies1")
                {
                    keyScript.targetObjectName = "LOCK1";
                }
                if (parentObject.name == "Enemies2")
                {
                    keyScript.targetObjectName = "LOCK2";
                }
                if (parentObject.name == "Enemies3")
                {
                    keyScript.targetObjectName = "LOCK3";
                }

            }
            Destroy(gameObject);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        Vector2 directionToPlayer = player.transform.position - transform.position;

        directionToPlayer.Normalize();

        direction = getDirection(directionToPlayer);

        playerScript = player.GetComponent<PlayerScript>();

        if (distanceToPlayer < 8 && !playerScript.ghost && distanceToPlayer > 3)
        {
            rb.velocity = directionToPlayer * speed * Time.fixedDeltaTime * 50;
        }
        else if (distanceToPlayer < 3 && playerScript.ghost)
        {
            rb.velocity = directionToPlayer * speed * Time.fixedDeltaTime * -50;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }


        if (distanceToPlayer < 8 && attackTime < 0)
        {
            attackTime = attackSpeed;

            bullet2 = Instantiate(bullet, transform.position, Quaternion.identity);

            Bullet spawnedScript = bullet2.GetComponent<Bullet>();
            if (spawnedScript != null)
            {
                spawnedScript.direction = directionToPlayer;
                spawnedScript.friendly = false;
            }
            Transform healthBarOne = transform.Find("HealthBarOne");
            if (healthBarOne != null)
            {
                healthBarOne.gameObject.SetActive(true);
            }
        }
        //else if (attackTime >= 0 && distanceToPlayer < 8)
        else if (attackTime >= 0)
        {
            attackTime -= Time.deltaTime;
            Transform healthBarOne = transform.Find("HealthBarOne");
            if (healthBarOne != null)
            {
                Transform bar = healthBarOne.Find("Bar");
                if (bar != null)
                {
                    Vector3 currentScale = bar.localScale;
                    Vector3 currentPosition = bar.localPosition;

                    currentScale.x = 0.9f * attackTime / attackSpeed;
                    currentPosition.x = -0.45f * (1 - (attackTime / attackSpeed));

                    bar.localScale = currentScale;
                    bar.localPosition = currentPosition;
                }
            }
        }

        if (attackTime <= 0)
        {
            Transform healthBarOne = transform.Find("HealthBarOne");
            if (healthBarOne != null)
            {
                healthBarOne.gameObject.SetActive(false);
            }
        }

        if (direction == 0)
        {
            ChangeAnimationState("Up");
        }
        else if (direction == 1)
        {
            ChangeAnimationState("Right");
        }
        else if(direction == 2)
        {
            ChangeAnimationState("Down");
        }
        else if(direction == 3)
        {
            ChangeAnimationState("Left");
        }
    }

    public void Hurt(int damage)
    {
        if (possessCooldown > 0)
        {
            return;
        }
        hurtTimer = 0.1f;
        health -= damage;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    public int getDirection(Vector2 direction)
    {
        return Mathf.Abs(direction.x) > Mathf.Abs(direction.y) ? direction.x >= 0 ? 1 : 3 : direction.y >= 0 ? 0 : 2;
    }
}
