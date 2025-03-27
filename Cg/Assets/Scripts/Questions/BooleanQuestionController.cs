using models;
using UnityEngine;

public class BooleanQuestionController : MonoBehaviour
{
    [SerializeField] private GameObject panelBoolean;

    public void ShowQuestion(PreguntasMultiples pregunta)
    {
        UIManager.Instance.HideAllPanels();
        panelBoolean.SetActive(true);
        UIManager.Instance.SetQuestionText(pregunta.Pregunta);
    }

    public void OnAnswerSelected(bool respuestaUsuario)
    {
        bool correcta = (respuestaUsuario && GetComponent<RoundManager>().currentQuestion.RespuestaCorrecta.ToLower() == "true") ||
                       (!respuestaUsuario && GetComponent<RoundManager>().currentQuestion.RespuestaCorrecta.ToLower() == "false");

        GetComponent<RoundManager>().RegisterAnswer(correcta, GetComponent<RoundManager>().currentQuestion.RespuestaCorrecta);
        panelBoolean.SetActive(false);
    }
}
