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
    private RoundManager roundManager;

    void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
        for (int i = 0; i < botones.Length; i++)
        {
            int index = i;
            if (botones[i] != null)
                botones[i].onClick.AddListener(() => VerifyAnswer(index));
            else
                Debug.LogError("Botón en el índice " + i + " no está asignado en MultipleChoiceController");
        }
    }

    public void ShowQuestion(PreguntasMultiples pregunta)
    {
        UIManager.Instance.HideAllPanels();
        panelMultiple.SetActive(true);
        UIManager.Instance.SetQuestionText(pregunta.Pregunta);
        preguntaActual = pregunta;
        ShuffleOptions();
    }

    void ShuffleOptions()
    {
        if (preguntaActual == null)
        {
            Debug.LogError("preguntaActual es null en ShuffleOptions");
            return;
        }
        List<string> respuestas = new List<string> {
            preguntaActual.Respuesta1,
            preguntaActual.Respuesta2,
            preguntaActual.Respuesta3,
            preguntaActual.Respuesta4
        };
        for (int i = respuestas.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            string temp = respuestas[i];
            respuestas[i] = respuestas[j];
            respuestas[j] = temp;
        }
        for (int i = 0; i < opciones.Length; i++)
        {
            if (i < respuestas.Count)
                opciones[i].text = respuestas[i];
            else
                Debug.LogWarning("No hay suficientes respuestas para asignar al índice " + i);
        }
    }

    void VerifyAnswer(int index)
    {
        if (preguntaActual == null)
        {
            Debug.LogError("preguntaActual es null en VerifyAnswer");
            return;
        }
        if (index < 0 || index >= opciones.Length)
        {
            Debug.LogError("Índice fuera de rango en VerifyAnswer: " + index);
            return;
        }
        string selectedAnswer = opciones[index].text;
        bool isCorrect = (selectedAnswer == preguntaActual.RespuestaCorrecta);
        if (roundManager != null)
            roundManager.RegisterAnswer(isCorrect, preguntaActual.RespuestaCorrecta);
        else
            Debug.LogError("roundManager es null en VerifyAnswer");
        panelMultiple.SetActive(false);
    }
}



