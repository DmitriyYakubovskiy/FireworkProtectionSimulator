using UnityEngine;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private PlayerTrigger playerTrigger;
    [SerializeField] private SoundController soundController;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float fadeDuration = 0.3f;

    private Transform player;
    private bool isVisible;

    private void Awake()
    {
        HidePanel();
        playerTrigger.OnPlayerEntered.AddListener(ShowPanel);
        playerTrigger.OnPlayerExited.AddListener(HidePanel);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isVisible && player != null)
        {
            Vector3 lookDirection = player.position - transform.position;
            lookDirection.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void ShowPanel()
    {
        isVisible = true;
        soundController.PlaySound(0, soundController.Volume);
        dialogPanel.SetActive(isVisible);
    }

    private void HidePanel()
    {
        isVisible = false;
        dialogPanel.SetActive(isVisible);
    }
}