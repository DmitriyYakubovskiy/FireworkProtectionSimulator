using UnityEngine;

public class NPS_LaunchRocket : MonoBehaviour
{
    [SerializeField] private Animator animator;
<<<<<<< HEAD:Assets/Scripts/Game/NPS_LaunchRocket.cs
    [SerializeField] private GameObject rocket;
    [SerializeField] private DialogueWindow dialogueWindow;
    private RocketLaunch button;
    void Start()
    {
        button = rocket.GetComponent<RocketLaunch>();
        
    }
=======
    [SerializeField] private GameObject[] rockets;

>>>>>>> 83fad3cec273c029d00074afeb1b110c57f5ac59:Assets/Main/Scripts/Game/NPS_LaunchRocket.cs
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
        }
    }
}
