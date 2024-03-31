using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    private Animator animator;

    public GameObject dingdong;
    public GameObject open;
    public GameObject buzz;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Ending1");
    }

    private void Update()
    {

    }

    public void Intro3()
    {
        animator.Play("Ending2");
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