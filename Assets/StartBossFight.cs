using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFight : MonoBehaviour
{
    public GameObject objectToToggle;
    public AudioSource soundSource;
    public AudioClip music;

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameObject.Find("LOCK3") != null)
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            DeleteAllRobots();
            objectToToggle.SetActive(true);
            soundSource.clip = music;
            soundSource.Play();
        }
    }

    private void DeleteObjectsWithTag(string tag)
    {
        GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objectsToDelete)
        {
            Destroy(obj);
        }
    }

    private void DeleteAllRobots()
    {
        DeleteObjectsWithTag("Robot1");
        DeleteObjectsWithTag("Robot2");
        DeleteObjectsWithTag("Robot3");
    }
}
