using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public List<GameObject> gunList = new List<GameObject>();
    public List<GameObject> bullets = new List<GameObject>();
    public List<float> bulletSpawnProbabilities = new List<float>();

    public int health;

    float hurtTimer;
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer fade;
    private GameObject player;
    public Material normal;
    private Rigidbody2D rb;

    public FollowScript cam;

    bool ending = false;

    float endingTick = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.forward * 0);

        if (hurtTimer > 0)
        {
            hurtTimer -= Time.deltaTime;
        }
        else
        {
            spriteRenderer.material = normal;
        }

        if (health <= 0 && !ending)
        {
            Transform healthBarOne = transform.Find("HealthBarOne");
            if (healthBarOne != null)
            {
                healthBarOne.gameObject.SetActive(false);
            }
            cam.target = transform;
            ending = true; 
        }

        if (ending)
        {
            endingTick -= Time.deltaTime;
            Color spriteColor = fade.color;
            spriteColor.a = 1-(endingTick / 3f);
            fade.color = spriteColor;
        }

        if (endingTick <= 0)
        {
            SceneManager.LoadScene("Ending");
        }

        if (!ending)
        {
            Vector2 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.Normalize();

            rb.velocity = directionToPlayer * 0.5f * Time.fixedDeltaTime * 50;

            // Check if it's time to spawn a bullet
            if (Random.Range(0f, 1f) < 0.08)
            {
                SpawnBullet();
            }

            Transform healthBarOne = transform.Find("HealthBarOne");
            if (healthBarOne != null)
            {
                Transform bar = healthBarOne.Find("Bar");
                if (bar != null)
                {
                    Vector3 currentScale = bar.localScale;
                    Vector3 currentPosition = bar.localPosition;

                    currentScale.x = 4f * health / 60;
                    //currentPosition.x = -2f * (1 - (health / 100)+1);

                    bar.localScale = currentScale;
                    bar.localPosition = currentPosition;
                }
            }
        }
    }

    public void Hurt(int damage)
    {
        hurtTimer = 0.1f;
        health -= damage;
    }

    private void SpawnBullet()
    {
        // Calculate the total probability sum
        float totalProbability = 0f;
        foreach (float probability in bulletSpawnProbabilities)
        {
            totalProbability += probability;
        }

        // Generate a random value between 0 and the total probability sum
        float randomValue = Random.Range(0f, totalProbability);

        int chosenBulletIndex = 8;
        float probabilitySum = 0f;
        if (CountRobots() <= 5)
        {
            for (int i = 0; i < bulletSpawnProbabilities.Count; i++)
            {
                probabilitySum += bulletSpawnProbabilities[i];
                if (randomValue <= probabilitySum)
                {
                    chosenBulletIndex = i;
                    break;
                }
            }
        }
        else if (CountRobots() > 5)
        {
            while (chosenBulletIndex > 2)
            {
                for (int i = 0; i < bulletSpawnProbabilities.Count; i++)
                {
                    probabilitySum += bulletSpawnProbabilities[i];
                    if (randomValue <= probabilitySum)
                    {
                        chosenBulletIndex = i;
                        break;
                    }
                }
            }
        }

        // Choose a random gun from the list
        int randomGunIndex = Random.Range(0, gunList.Count);
        GameObject randomGun = gunList[randomGunIndex];

        // Get the chosen bullet based on the index
        GameObject chosenBullet = bullets[chosenBulletIndex];

        // Spawn the bullet at the gun's position and rotation
        GameObject bullet = Instantiate(chosenBullet, randomGun.transform.position, randomGun.transform.rotation);

        if (chosenBulletIndex <= 2)
        {
            Bullet spawnedScript = bullet.GetComponent<Bullet>();
            if (spawnedScript != null)
            {
                spawnedScript.direction = (randomGun.transform.position - transform.position).normalized;
                spawnedScript.friendly = false;
            }
        }
        else
        {
            FlingScript spawnedScript = bullet.GetComponent<FlingScript>();
            if (spawnedScript != null)
            {
                spawnedScript.direction = (randomGun.transform.position - transform.position).normalized;
            }
        }
    }

    private int CountRobots()
    {
        int robotCount = 0;

        GameObject[] robot1Objects = GameObject.FindGameObjectsWithTag("Fling");
        robotCount += robot1Objects.Length;

        GameObject[] robot2Objects = GameObject.FindGameObjectsWithTag("Robot1");
        robotCount += robot2Objects.Length;

        GameObject[] robot3Objects = GameObject.FindGameObjectsWithTag("Robot2");
        robotCount += robot3Objects.Length;

        GameObject[] robot4Objects = GameObject.FindGameObjectsWithTag("Robot3");
        robotCount += robot4Objects.Length;

        return robotCount;
    }

}