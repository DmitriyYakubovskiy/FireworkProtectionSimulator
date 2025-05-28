using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button[] choiceButtons;
    [SerializeField] private TextAsset dialogueJson;
    [SerializeField] private GameObject rockets;//TODO
    private RocketLauncher rocketLauncher;
    private ScoreManager scoreManager;

    private DialogueData dialogueData;
    private Dictionary<int, DialogueNode> nodeLookup = new Dictionary<int, DialogueNode>();

    [Serializable]
    public class DialogueNode
    {
        public int id;
        public string text;
        public string dialogueEvent;
        public Choice[] choices;
    }

    [Serializable]
    public class Choice
    {
        public string text;
        public int nextNodeId;
    }

    [Serializable]
    private class DialogueData
    {
        public DialogueNode[] nodes;
    }

    private void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        dialogueData = JsonUtility.FromJson<DialogueData>(dialogueJson.text);
        foreach (var node in dialogueData.nodes)
        {
            nodeLookup[node.id] = node;
        }

        // Скрываем кнопки выбора
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }

        if (rockets != null)
        {
            rocketLauncher = rockets.GetComponent<RocketLauncher>();
        }
        // Начинаем диалог
        StartDialogue(0);
    }

    private void StartDialogue(int nodeId)
    {
        if (!nodeLookup.ContainsKey(nodeId))
        {
            Debug.Log("Диалог завершен");
            return;
        }

        DialogueNode currentNode = nodeLookup[nodeId];
        dialogueText.text = currentNode.text;

        Debug.Log($"Диалог {currentNode.dialogueEvent}");
        if (!string.IsNullOrEmpty(currentNode.dialogueEvent))
        {
            HandleEvent(currentNode.dialogueEvent);
        }

        // Настраиваем кнопки выбора
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < currentNode.choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentNode.choices[i].text;
                int nextNodeId = currentNode.choices[i].nextNodeId;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => StartDialogue(nextNodeId));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void HandleEvent(string eventName)
    {
        switch (eventName)
        {
            case "meet_MCHS":
                Debug.Log("dialogue_init");
                break;

            case "clean_fireworks":
                Destroy(rocketLauncher.gameObject);
                Destroy(gameObject, 5);
                break;

            case "lose_1":
                scoreManager.BadScore+=1;
                Destroy(gameObject, 5);
                break;  

            case "lose_with_start_rockets_1":
                scoreManager.BadScore += 1;
                rocketLauncher.Launch();
                Destroy(gameObject, 5);
                break;

            case "success_1":
                scoreManager.GoodScore += 1;
                Destroy(gameObject, 5);
                break;

            case "success_with_start_rockets_1":
                scoreManager.GoodScore += 1;
                rocketLauncher.Launch();
                Destroy(gameObject, 5);
                break;

            default:
                Debug.LogWarning($"Неизвестное событие: {eventName}");
                break;
        }
    }
}