using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel; // Панель диалога
    [SerializeField] private Text dialogueText; // Текст диалога
    [SerializeField] private float offsetY = 2f; // Смещение по Y над объектом
    [SerializeField] private float displayTime = 3f; // Время отображения

    private Camera mainCamera;
    private bool isShowing = false;

    private void Start()
    {
        if (isShowing)
        {
            // Конвертируем мировые координаты объекта в экранные
            Vector3 worldPos = transform.position + Vector3.up * offsetY;
            Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

            // Устанавливаем позицию панели диалога
            dialoguePanel.transform.position = screenPos;
        }
    }

    private void Update()
    {
        
    }

    public void ShowDialogue(string message)
    {
        if (isShowing) return;

        dialogueText.text = message;
        dialoguePanel.SetActive(true);
        isShowing = true;

        // Запускаем корутину для скрытия через время
        StartCoroutine(HideAfterTime());
    }

    private System.Collections.IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(displayTime);
        HideDialogue();
    }

    private void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        isShowing = false;
    }
}