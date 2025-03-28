using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float timeRemaining;
    private bool timerIsRunning;
    private System.Action onTimeUp;

    void Start()
    {
        if (timerText == null)
        {
            GameObject obj = GameObject.Find("TxtTimer");
            if (obj != null)
                timerText = obj.GetComponent<TextMeshProUGUI>();
            if (timerText == null)
                Debug.LogError("El componente TextMeshProUGUI 'TxtTimer' no se encontró ni está asignado en Timer.");
        }
    }

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
        if (timeRemaining < 0) timeRemaining = 0;
        UpdateTimerDisplay();
        if (timeRemaining <= 0)
        {
            timerIsRunning = false;
            onTimeUp?.Invoke();
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText == null) return;
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}


