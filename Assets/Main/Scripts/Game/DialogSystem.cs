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
    [SerializeField] private GameObject rockets;
    private RocketLauncher rocketLauncher;


    [System.Serializable]
    public class DialogueNode
    {
        public int id;
        public string text;
        public string @event; // Зарезервированное слово event, используем @event
        public Choice[] choices;
    }

    [System.Serializable]
    public class Choice
    {
        public string text;
        public int nextNodeId;
    }

    [System.Serializable]
    private class DialogueData
    {
        public DialogueNode[] nodes;
    }

    private DialogueData dialogueData;
    private Dictionary<int, DialogueNode> nodeLookup = new Dictionary<int, DialogueNode>();

    void Start()
    {
        // Загружаем и парсим JSON
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

        rocketLauncher = rockets.GetComponent<RocketLauncher>();

        // Начинаем диалог
        StartDialogue(0);
    }

    void StartDialogue(int nodeId)
    {
        if (!nodeLookup.ContainsKey(nodeId))
        {
            Debug.Log("Диалог завершен");
            return;
        }

        DialogueNode currentNode = nodeLookup[nodeId];
        dialogueText.text = currentNode.text;

        // Обрабатываем событие, если оно указано
        if (!string.IsNullOrEmpty(currentNode.@event))
        {
            HandleEvent(currentNode.@event);
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

    void HandleEvent(string eventName)
    {
        // Пример обработки событий
        switch (eventName)
        {
            case "meet_MCHS":
                Debug.Log("Познакомился с МЧС");
                // Здесь можно открыть новую локацию или обновить UI
                break;
            case "clean_fireworks":
                Debug.Log("Начался квест: Испытание героя!");
                // Здесь можно активировать квест в системе квестов
                break;
            case "lose":
                Debug.Log("Салюты запускаются");
                rocketLauncher.Launch();
                break;
            case "succes":
                Debug.Log("Игрок направляется в темный лес!");
                // Здесь можно загрузить новую сцену или активировать триггер
                break;
            default:
                Debug.LogWarning($"Неизвестное событие: {eventName}");
                break;
        }
    }
}