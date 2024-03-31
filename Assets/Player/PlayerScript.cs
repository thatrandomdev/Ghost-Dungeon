using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    Vector2 movement;
    Vector2 movement2;
    public bool ghost = true;

    public float maxSpeed = 5f;
    public float acceleration = 2f;
    public float deceleration = 0.9f;

    private Vector2 currentVelocity;

    private Animator animator;

    private string currentState;

    public float hurtTick = -0.1f;

    public Vector2 hurtDirection;

    int direction;

    public int robot;

    public GameObject swapPlayer;

    private GameObject bullet;

    public GameObject bigBulletObject;
    public GameObject mediumBulletObject;
    public GameObject smallBulletObject;
    float attackTime;

    public int room = 1;

    public AudioSource woo;

    bool wooPlaying = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.forward * 0);

        if (Input.GetMouseButton(0) && robot == 1 && attackTime <= 0)
        {
            attackTime = 1;

            bullet = Instantiate(bigBulletObject, transform.position, Quaternion.identity);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3 direction2 = mousePosition - transform.position;
            direction2 = direction2.normalized;

            Bullet spawnedScript = bullet.GetComponent<Bullet>();
            if (spawnedScript != null)
            {
                spawnedScript.direction = direction2;
                spawnedScript.friendly = true;
            }
            Transform healthBarOne = transform.Find("HealthBarOne");
            if (healthBarOne != null)
            {
                healthBarOne.gameObject.SetActive(true);
            }
        }
        if (Input.GetMouseButton(0) && robot == 2 && attackTime <= 0)
        {
            attackTime = 0.5f;

            bullet = Instantiate(mediumBulletObject, transform.position, Quaternion.identity);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3 direction2 = mousePosition - transform.position;
            direction2 = direction2.normalized;

            Bullet spawnedScript = bullet.GetComponent<Bullet>();
            if (spawnedScript != null)
            {
                spawnedScript.direction = direction2;
                spawnedScript.friendly = true;
            }
            Transform healthBarTwo = transform.Find("HealthBarTwo");
            if (healthBarTwo != null)
            {
                healthBarTwo.gameObject.SetActive(true);
            }
        }
        if (Input.GetMouseButton(0) && robot == 3 && attackTime <= 0)
        {
            attackTime = 0.3f;

            bullet = Instantiate(smallBulletObject, transform.position, Quaternion.identity);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3 direction2 = mousePosition - transform.position;
            direction2 = direction2.normalized;

            Bullet spawnedScript = bullet.GetComponent<Bullet>();
            if (spawnedScript != null)
            {
                spawnedScript.direction = direction2;
                spawnedScript.friendly = true;
            }
            Transform healthBarThree = transform.Find("HealthBarThree");
            if (healthBarThree != null)
            {
                healthBarThree.gameObject.SetActive(true);
            }
        }
        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
            if (robot == 1)
            {
                Transform healthBarOne = transform.Find("HealthBarOne");
                if (healthBarOne != null)
                {
                    Transform bar = healthBarOne.Find("Bar");
                    if (bar != null)
                    {
                        Vector3 currentScale = bar.localScale;
                        Vector3 currentPosition = bar.localPosition;

                        currentScale.x = 0.9f * attackTime/1;
                        currentPosition.x = -0.45f * (1 - (attackTime / 1));

                        bar.localScale = currentScale;
                        bar.localPosition = currentPosition;
                    }
                }
            }
            if (robot == 2)
            {
                Transform healthBarTwo = transform.Find("HealthBarTwo");
                if (healthBarTwo != null)
                {
                    Transform bar = healthBarTwo.Find("Bar");
                    if (bar != null)
                    {
                        Vector3 currentScale = bar.localScale;
                        Vector3 currentPosition = bar.localPosition;

                        currentScale.x = 0.85f * attackTime / 0.5f;
                        currentPosition.x = -0.425f * (1 - (attackTime / 0.5f));

                        bar.localScale = currentScale;
                        bar.localPosition = currentPosition;
                    }
                }
            }
            if (robot == 3)
            {
                Transform healthBarThree = transform.Find("HealthBarThree");
                if (healthBarThree != null)
                {
                    Transform bar = healthBarThree.Find("Bar");
                    if (bar != null)
                    {
                        Vector3 currentScale = bar.localScale;
                        Vector3 currentPosition = bar.localPosition;

                        currentScale.x = 0.8f * attackTime / 0.3f;
                        currentPosition.x = -0.4f * (1 - (attackTime / 0.3f));

                        bar.localScale = currentScale;
                        bar.localPosition = currentPosition;
                    }
                }
            }
        }
        else
        {
            Transform healthBarOne = transform.Find("HealthBarOne");
            if (healthBarOne != null)
            {
                healthBarOne.gameObject.SetActive(false);
            }

            Transform healthBarTwo = transform.Find("HealthBarTwo");
            if (healthBarTwo != null)
            {
                healthBarTwo.gameObject.SetActive(false);
            }

            Transform healthBarThree = transform.Find("HealthBarThree");
            if (healthBarThree != null)
            {
                healthBarThree.gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {

        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");

        Vector2 inputMovement = new Vector2(mx, my).normalized;

        if (ghost)
        {
                if (inputMovement != Vector2.zero)
                {
                    if (!wooPlaying)
                    {
                        woo.Play();
                    }
                    wooPlaying = true;
                    currentVelocity = inputMovement * maxSpeed;
                }
                else
                {
                    if (wooPlaying)
                    {
                        woo.Stop();
                    }
                    wooPlaying = false;
                    currentVelocity *= deceleration;
                }

                rb.velocity = Vector2.Lerp(rb.velocity, currentVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = inputMovement * speed * Time.fixedDeltaTime * 50;
        }

        if (hurtTick > 0)
        {
            hurtTick -= Time.deltaTime;
        }

        //if (inputMovement != Vector2.zero)
        if (true)
        {
            direction = getDirection(inputMovement);
            if (inputMovement == Vector2.zero)
            {
                direction = 4;
            }

            if (ghost && hurtTick > 0)
            {
                ChangeAnimationState("OW");
            }
            else if (ghost && hurtTick <= 0)
            {
                if (direction == 0)
                {
                    ChangeAnimationState("Up");
                }
                else if (direction == 1)
                {
                    ChangeAnimationState("Right");
                }
                else if (direction == 2)
                {
                    ChangeAnimationState("Down");
                }
                else if (direction == 3)
                {
                    ChangeAnimationState("Left");
                }
                else if (direction == 4)
                {
                    ChangeAnimationState("Idle");
                }
            }
            else if (robot == 1)
            {
                if (direction == 0)
                {
                    ChangeAnimationState("Up1");
                }
                else if (direction == 1)
                {
                    ChangeAnimationState("Right1");
                }
                else if (direction == 2)
                {
                    ChangeAnimationState("Down1");
                }
                else if (direction == 3)
                {
                    ChangeAnimationState("Left1");
                }
                else if (direction == 4)
                {
                    ChangeAnimationState("Idle1");
                }
            }
            else if (robot == 2)
            {
                if (direction == 0)
                {
                    ChangeAnimationState("Up2");
                }
                else if (direction == 1)
                {
                    ChangeAnimationState("Right2");
                }
                else if (direction == 2)
                {
                    ChangeAnimationState("Down2");
                }
                else if (direction == 3)
                {
                    ChangeAnimationState("Left2");
                }
                else if (direction == 4)
                {
                    ChangeAnimationState("Idle2");
                }
            }
            else if (robot == 3)
            {
                if (direction == 0)
                {
                    ChangeAnimationState("Up3");
                }
                else if (direction == 1)
                {
                    ChangeAnimationState("Right3");
                }
                else if (direction == 2)
                {
                    ChangeAnimationState("Down3");
                }
                else if (direction == 3)
                {
                    ChangeAnimationState("Left3");
                }
                else if (direction == 4)
                {
                    ChangeAnimationState("Idle3");
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool canPossess = false;

        EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();

        if (enemyScript != null)
        {
            if (enemyScript.possessCooldown <= 0)
            {
                canPossess = true;
            }
        }
        else if (enemyScript == null)
        {
            canPossess = true;
        }
        if (collision.gameObject.CompareTag("Robot1") && ghost && canPossess)
        {
            Instantiate(swapPlayer, transform.position, Quaternion.identity);
            transform.position = collision.transform.position;
            Destroy(collision.gameObject);
            ghost = false;
            speed = 2f;
            robot = 1;
            ChangeAnimationState("Idle1");
            if (wooPlaying)
            {
                woo.Stop();
            }
            wooPlaying = false;
        }
        if (collision.gameObject.CompareTag("Robot2") && ghost && canPossess)
        {
            Instantiate(swapPlayer, transform.position, Quaternion.identity);
            transform.position = collision.transform.position;
            Destroy(collision.gameObject);
            ghost = false;
            speed = 3.5f;
            robot = 2;
            ChangeAnimationState("Idle2");
            if (wooPlaying)
            {
                woo.Stop();
            }
            wooPlaying = false;
        }
        if (collision.gameObject.CompareTag("Robot3") && ghost && canPossess)
        {
            Instantiate(swapPlayer, transform.position, Quaternion.identity);
            transform.position = collision.transform.position;
            Destroy(collision.gameObject);
            ghost = false;
            speed = 4f;
            robot = 3;
            ChangeAnimationState("Idle3");
            if (wooPlaying)
            {
                woo.Stop();
            }
            wooPlaying = false;
        }
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
