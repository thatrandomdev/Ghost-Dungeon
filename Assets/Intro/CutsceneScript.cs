using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScript : MonoBehaviour
{
    private Animator animator;
    bool ready = false;

    public GameObject dingdong;
    public GameObject open;
    public GameObject buzz;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ready)
            {
                SceneManager.LoadScene("Main");
            }
            else
            {
                animator.Play("Intro2");
            }
        }
    }

    public void Intro3()
    {
        animator.Play("Intro3");
        ready = true;
    }

    public void DingDong()
    {
        Instantiate(dingdong, transform.position, Quaternion.identity);
    }
    public void Open()
    {
        Instantiate(open, transform.position, Quaternion.identity);
    }
    public void Buzz()
    {
        Instantiate(buzz, transform.position, Quaternion.identity);
    }
}