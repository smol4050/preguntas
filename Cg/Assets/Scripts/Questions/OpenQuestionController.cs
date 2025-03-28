using models;
using TMPro;
using UnityEngine;

public class OpenQuestionController : MonoBehaviour
{
    [SerializeField] private GameObject panelAbierta;
    [SerializeField] private TMP_InputField inputField;
    private RoundManager roundManager;
    void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
    }
    public void ShowQuestion(PreguntasAbiertas pregunta)
    {
        UIManager.Instance.HideAllPanels();
        panelAbierta.SetActive(true);
        UIManager.Instance.SetQuestionText(pregunta.Pregunta);
        inputField.text = "";
    }

    public void VerifyOpenAnswer()
    {
        string respuestaUsuario = inputField.text.Trim().ToLower();
        string respuestaCorrecta = roundManager.currentQuestion.RespuestaCorrecta.Trim().ToLower();
        bool isCorrect = respuestaUsuario == respuestaCorrecta;
        roundManager.RegisterAnswer(isCorrect, roundManager.currentQuestion.RespuestaCorrecta);
        panelAbierta.SetActive(false);
    }
}


