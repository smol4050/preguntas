using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float timeRemaining;
    private bool timerIsRunning;
    private System.Action onTimeUp;

    public void StartTimer(float duration, System.Action callback)
    {
        timeRemaining = duration;
        timerIsRunning = true;
        onTimeUp = callback;
    }

    void Update()
    {
        if (!timerIsRunning) return;

        timeRemaining -= Time.deltaTime;
        UpdateTimerDisplay();

        if (timeRemaining <= 0)
        {
            timerIsRunning = false;
            onTimeUp?.Invoke();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}