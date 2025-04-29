using UnityEngine;

public class NPS_LaunchRocket : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] rockets;
    [SerializeField] private DialogueWindow dialogueWindow;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueWindow.ShowDialogue("privet");
            animator.SetTrigger("LaunchRocket");
            for (int i = 0; i < rockets.Length; i++)
            {
                rockets[i].GetComponent<RocketLaunch>().StartFly();
            }
            rockets = null;
        }
    }
}
