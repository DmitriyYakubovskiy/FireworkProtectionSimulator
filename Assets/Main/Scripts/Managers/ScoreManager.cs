using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int maxScore;
    private int goodScore = 0;
    private int badScore = 0;
    private int score = 0;

    public int BadScore
    {
        get
        {
            return badScore;
        }
        set
        {
            if (badScore != value)
            {
                badScore = value;
                OnScoreChanging.Invoke(goodScore, badScore);
            }
        }
    }

    public int GoodScore
    {
        get
        {
            return goodScore;
        }
        set
        {
            if (goodScore != value)
            {
                goodScore = value;
                OnScoreChanging.Invoke(goodScore, badScore);
            }
        }
    }

    public UnityEvent<int, int> OnScoreChanging = new UnityEvent<int, int>();

    private void Start()
    {
        OnScoreChanging.AddListener(ChangeScore);
    }

    private void ChangeScore(int goodScore, int badScore)
    {
        Debug.Log($"Количество правильных ответов: {goodScore}\nКоличество неправильных ответов: {badScore}\nЭффективность: {Mathf.Round(goodScore/(maxScore))}");
        scoreText.text = $"Количество правильных ответов: {goodScore}\nКоличество неправильных ответов: {badScore}\nЭффективность: {Mathf.Round(((goodScore / (float)maxScore) * 100))}%";
    }

    private void OnDestroy()
    {
        OnScoreChanging.RemoveListener(ChangeScore);
    }
}

