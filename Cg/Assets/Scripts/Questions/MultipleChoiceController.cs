using models;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceController : MonoBehaviour
{
    [SerializeField] private GameObject panelMultiple;
    [SerializeField] private TextMeshProUGUI[] opciones;
    [SerializeField] private Button[] botones;
    private PreguntasMultiples preguntaActual;

    void Start()
    {
        for (int i = 0; i < botones.Length; i++)
        {
            int index = i;
            botones[i].onClick.AddListener(() => VerifyAnswer(index));
        }
    }

    public void ShowQuestion(PreguntasMultiples pregunta)
    {
        UIManager.Instance.HideAllPanels();
        panelMultiple.SetActive(true);
        UIManager.Instance.SetQuestionText(pregunta.Pregunta);
        preguntaActual = pregunta;
        ShuffleOptions(pregunta);
    }

    void ShuffleOptions(PreguntasMultiples pregunta)
    {
        List<string> respuestas = new List<string>{
            pregunta.Respuesta1,
            pregunta.Respuesta2,
            pregunta.Respuesta3,
            pregunta.Respuesta4
        };

        for (int i = respuestas.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            string temp = respuestas[i];
            respuestas[i] = respuestas[j];
            respuestas[j] = temp;
        }

        for (int i = 0; i < opciones.Length; i++)
            opciones[i].text = respuestas[i];
    }

    void VerifyAnswer(int index)
    {
        bool isCorrect = opciones[index].text == preguntaActual.RespuestaCorrecta;
        GetComponent<RoundManager>().RegisterAnswer(isCorrect, preguntaActual.RespuestaCorrecta);
        panelMultiple.SetActive(false);
    }
}
