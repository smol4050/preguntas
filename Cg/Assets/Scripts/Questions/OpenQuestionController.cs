using models;
using TMPro;
using UnityEngine;

public class OpenQuestionController : MonoBehaviour
{
    [SerializeField] private GameObject panelAbierta;
    [SerializeField] private TMP_InputField inputField;

    public void ShowQuestion(PreguntasMultiples pregunta)
    {
        UIManager.Instance.HideAllPanels();
        panelAbierta.SetActive(true);
        UIManager.Instance.SetQuestionText(pregunta.Pregunta);
        inputField.text = "";
    }

    public void VerifyOpenAnswer()
    {
        string respuestaUsuario = inputField.text.Trim().ToLower();
        string respuestaCorrecta = GetComponent<RoundManager>().currentQuestion.RespuestaCorrecta.Trim().ToLower();

        bool isCorrect = respuestaUsuario == respuestaCorrecta;
        GetComponent<RoundManager>().RegisterAnswer(isCorrect, GetComponent<RoundManager>().currentQuestion.RespuestaCorrecta);
        panelAbierta.SetActive(false);
    }
}
