using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject panelMultiple;
    [SerializeField] private GameObject panelAbierta;
    [SerializeField] private GameObject panelBooleana;
    [SerializeField] private GameObject panelResultado;

    [SerializeField] private TextMeshProUGUI txtPregunta;
    [SerializeField] private TextMeshProUGUI txtResultado;
    [SerializeField] private TextMeshProUGUI txtRonda;
    [SerializeField] private TextMeshProUGUI txtPreguntaNumber;
    [SerializeField] private TextMeshProUGUI txtCorrectas;
    [SerializeField] private TextMeshProUGUI txtIncorrectas;

    private int correctas = 0;
    private int incorrectas = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        HideAllPanels();
        txtPregunta.text = "";
        txtResultado.text = "";
        txtRonda.text = "";
        txtPreguntaNumber.text = "";
        txtCorrectas.text = "Correctas: 0";
        txtIncorrectas.text = "Incorrectas: 0";
    }

    void OnEnable()
    {
        RoundManager.OnRoundStart += HideAllPanels;
        RoundManager.OnQuestionAnswered += UpdateResultUI;
    }

    void OnDisable()
    {
        RoundManager.OnRoundStart -= HideAllPanels;
        RoundManager.OnQuestionAnswered -= UpdateResultUI;
    }

    public void HideAllPanels()
    {
        panelMultiple.SetActive(false);
        panelAbierta.SetActive(false);
        panelBooleana.SetActive(false);
        panelResultado.SetActive(false);
    }

    
    public void UpdateResultUI(bool isCorrect, string correctAnswer)
    {
        HideAllPanels();
        panelResultado.SetActive(true);
        if (isCorrect)
            correctas++;
        else
            incorrectas++;
        txtCorrectas.text = "Correctas: " + correctas.ToString();
        txtIncorrectas.text = "Incorrectas: " + incorrectas.ToString();
        txtResultado.text = isCorrect ? "¡Correcto! :)" : $"Incorrecto :(\nRespuesta: {correctAnswer}";
    }

    
    public void ShowFinalResult(bool success, int correct, int total, int round)
    {
        HideAllPanels();
        panelResultado.SetActive(true);
        txtRonda.text = "Ronda: " + (round + 1).ToString();
        string message = "";
        if (success)
        {
            message = $"¡Felicidades!\nObtuviste {correct}/{total} correctas.\nLa próxima ronda comenzará en 10 segundos.";
        }
        else
        {
            message = $"Inténtalo nuevamente.\nObtuviste {correct}/{total} correctas.\nRegresando al inicio...";
        }
        txtResultado.text = message;
    }

    public void SetQuestionText(string text)
    {
        txtPregunta.text = text;
    }

    public void UpdateQuestionNumber(int currentQuestionNumber, int totalQuestions)
    {
        if (txtPreguntaNumber != null)
            txtPreguntaNumber.text = $"Pregunta: {currentQuestionNumber}/{totalQuestions}";
    }

    public void SetRoundNumber(int round)
    {
        if (txtRonda != null)
            txtRonda.text = "Ronda: " + round.ToString();
    }
}
