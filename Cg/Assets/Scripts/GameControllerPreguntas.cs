using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using models;
using System.IO;
using System;
using TMPro;

public class GameControllerPreguntas : MonoBehaviour
{
    List<PreguntasMultiples> listaPM;
    string lineaLeida;

    public TextMeshProUGUI txtpregunta;
    public TextMeshProUGUI txtrespuesta1;
    public TextMeshProUGUI txtrespuesta2;
    public TextMeshProUGUI txtrespuesta3;
    public TextMeshProUGUI txtrespuesta4;
    string respuestaCorrecta;
    // Start is called before the first frame update
    void Start()
    {
        listaPM = new List<PreguntasMultiples>();
        leerPreguntasMultiples();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void mostrarPregunta()
    {
        txtpregunta.text = listaPM[0].Pregunta;
        txtrespuesta1.text = listaPM[0].Respuesta1;
        txtrespuesta2.text = listaPM[0].Respuesta2;
        txtrespuesta3.text = listaPM[0].Respuesta3;
        txtrespuesta4.text = listaPM[0].Respuesta4;
        respuestaCorrecta = listaPM[0].RespuestaCorrecta;
    }

    public void respuesta1()
    {
        if (txtrespuesta1.text == respuestaCorrecta)
        {
            Debug.Log("Respuesta correcta");
        }
        else
        {
            Debug.Log("Respuesta incorrecta");
        }
    }
    public void respuesta2()
    {
        if (txtrespuesta2.text == respuestaCorrecta)
        {
            Debug.Log("Respuesta correcta");
        }
        else
        {
            Debug.Log("Respuesta incorrecta");
        }
    }

    public void respuesta3()
    {
        if (txtrespuesta3.text == respuestaCorrecta)
        {
            Debug.Log("Respuesta correcta");
        }
        else
        {
            Debug.Log("Respuesta incorrecta");
        }
    }

    public void respuesta4()
    {
        if (txtrespuesta4.text == respuestaCorrecta)
        {
            Debug.Log("Respuesta correcta");
        }
        else
        {
            Debug.Log("Respuesta incorrecta");
        }
    }

    #region LeerPreguntas
    public void leerPreguntasMultiples()
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Resources/Files/ArchivoPreguntasM.txt");
            while ((lineaLeida = sr1.ReadLine()) != null)
            {
                string[] lineapartida = lineaLeida.Split("-");
                string pregunta = lineapartida[0];
                string respuesta1 = lineapartida[1];
                string respuesta2 = lineapartida[2];
                string respuesta3 = lineapartida[3];
                string respuesta4 = lineapartida[4];
                string respuestaCorrecta = lineapartida[5];
                string versiculo = lineapartida[6];
                string dicultad = lineapartida[7];

                PreguntasMultiples objPM = new PreguntasMultiples(pregunta, respuesta1, respuesta2,
                    respuesta3, respuesta4, respuestaCorrecta, versiculo, dicultad);

                listaPM.Add(objPM);

            }
            Debug.Log("Tamaño lista preguntas muliples "+listaPM.Count);
        }
        catch (Exception e)
        {
            Debug.Log("ERROR " + e.ToString());
        }
    }

}
#endregion