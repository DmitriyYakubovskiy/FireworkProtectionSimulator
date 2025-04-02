using UnityEngine;

public class NPS_LaunchRocket : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] rockets;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("LaunchRocket");
            for (int i = 0; i < rockets.Length; i++)
            {
                rockets[i].GetComponent<RocketLaunch>().StartFly();
            }
        }
    }
}
