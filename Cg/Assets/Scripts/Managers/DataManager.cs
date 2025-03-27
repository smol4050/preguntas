using System.Collections.Generic;
using System.IO;
using UnityEngine;
using models;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public List<PreguntasMultiples> LoadQuestions(string fileName, bool allowEasy, bool allowHard)
    {
        List<PreguntasMultiples> questions = new List<PreguntasMultiples>();
        string path = Path.Combine(Application.dataPath, "Scripts/PM", fileName);

        if (!File.Exists(path))
        {
            Debug.LogError($"Archivo no encontrado: {path}");
            return questions;
        }

        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split('-');
                if (data.Length < 8) continue;

                PreguntasMultiples question = new PreguntasMultiples(
                    data[0], data[1], data[2], data[3], data[4],
                    data[5], data[6], data[7]);

                if ((allowEasy && question.Dificultad.ToLower() == "facil") ||
                    (allowHard && question.Dificultad.ToLower() == "dificil"))
                {
                    questions.Add(question);
                }
            }
        }
        return questions;
    }
}