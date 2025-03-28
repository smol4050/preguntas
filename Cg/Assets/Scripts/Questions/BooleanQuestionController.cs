using models;
using UnityEngine;

public class BooleanQuestionController : MonoBehaviour
{
    [SerializeField] private GameObject panelBoolean;
    private RoundManager roundManager;

    void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
    }

    public void ShowQuestion(PreguntasVerdaderoFalso pregunta)
    {
        UIManager.Instance.HideAllPanels();
        panelBoolean.SetActive(true);
        UIManager.Instance.SetQuestionText(pregunta.Pregunta);
    }

    public void OnAnswerSelected(bool respuestaUsuario)
    {
        bool correcta = (respuestaUsuario && roundManager.currentQuestion.RespuestaCorrecta.ToLower() == "true") ||
                         (!respuestaUsuario && roundManager.currentQuestion.RespuestaCorrecta.ToLower() == "false");
        roundManager.RegisterAnswer(correcta, roundManager.currentQuestion.RespuestaCorrecta);
        if (correcta)
            AudioManager.Instance.PlayCorrect();
        else
            AudioManager.Instance.PlayIncorrect();
        panelBoolean.SetActive(false);
    }

    public void SelectTrue()
    {
        OnAnswerSelected(true);
    }

    public void SelectFalse()
    {
        OnAnswerSelected(false);
    }
}
