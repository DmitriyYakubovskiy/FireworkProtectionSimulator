using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            if (score != value)
            {
                score = value;
                OnScoreChanging.Invoke(score);
            }
        }
    }

    public UnityEvent<int> OnScoreChanging = new UnityEvent<int>();

    private void Start()
    {
        OnScoreChanging.AddListener(ChangeScore);
    }

    private void ChangeScore(int score)
    {
        Debug.Log($"Score changing to: {score}");
        scoreText.text = score.ToString();
    }

    private void OnDestroy()
    {
        OnScoreChanging.RemoveListener(ChangeScore);
    }
}
