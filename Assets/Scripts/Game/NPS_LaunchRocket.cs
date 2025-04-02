using UnityEngine;

public class NPS_LaunchRocket : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject rocket;
    private RocketLaunch button;
    void Start()
    {
        button = rocket.GetComponent<RocketLaunch>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("LaunchRocket");
            button.StartFly();
        }
    }
}
