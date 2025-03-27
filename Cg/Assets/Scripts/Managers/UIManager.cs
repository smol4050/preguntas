using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Paneles")]
    [SerializeField] private GameObject panelMultiple;
    [SerializeField] private GameObject panelAbierta;
    [SerializeField] private GameObject panelBooleana;
    [SerializeField] private GameObject panelResultado;

    [Header("Textos")]
    [SerializeField] private TextMeshProUGUI txtPregunta;
    [SerializeField] private TextMeshProUGUI txtResultado;
    [SerializeField] private TextMeshProUGUI txtRonda;
    [SerializeField] private TextMeshProUGUI txtCorrectas;
    [SerializeField] private TextMeshProUGUI txtIncorrectas;

    private int correctas = 0;
    private int incorrectas = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        RoundManager.OnRoundStart += HideAllPanels;
        RoundManager.OnQuestionAnswered += ShowResult;
    }

    void OnDisable()
    {
        RoundManager.OnRoundStart -= HideAllPanels;
        RoundManager.OnQuestionAnswered -= ShowResult;
    }

    public void HideAllPanels()
    {
        panelMultiple.SetActive(false);
        panelAbierta.SetActive(false);
        panelBooleana.SetActive(false);
        panelResultado.SetActive(false);
    }

    public void ShowResult(bool isCorrect, string correctAnswer)
    {
        HideAllPanels();
        panelResultado.SetActive(true);
        txtResultado.text = isCorrect ? "¡Correcto! :)" : $"Incorrecto :(\nRespuesta: {correctAnswer}";

        if (isCorrect) correctas++;
        else incorrectas++;

        txtCorrectas.text = correctas.ToString();
        txtIncorrectas.text = incorrectas.ToString();
    }

    public void SetQuestionText(string text) => txtPregunta.text = text;
}