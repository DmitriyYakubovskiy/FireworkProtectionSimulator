using UnityEngine;

public class NPS_LaunchRocket : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject rocket;
    [SerializeField] private DialogueWindow dialogueWindow;
    private RocketLaunch button;
    void Start()
    {
        button = rocket.GetComponent<RocketLaunch>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueWindow.ShowDialogue("privet");
            animator.SetTrigger("LaunchRocket");
            button.StartPolet();
        }
    }
}
