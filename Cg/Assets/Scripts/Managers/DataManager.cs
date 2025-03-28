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
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    string CleanFileName(string fileName)
    {
        fileName = fileName.Trim().Replace("\n", "").Replace("\r", "");
        foreach (char c in System.IO.Path.GetInvalidFileNameChars())
        {
            fileName = fileName.Replace(c.ToString(), "");
        }
        if (!System.IO.Path.HasExtension(fileName))
        {
            fileName += ".txt";
        }
        return fileName;
    }

    public List<PreguntasMultiples> LoadPreguntasMultiples(string fileName, bool allowEasy, bool allowHard)
    {
        List<PreguntasMultiples> preguntas = new List<PreguntasMultiples>();
        fileName = CleanFileName(fileName);
        string path = Path.Combine(Application.dataPath, "Resources", "Files", fileName);
        if (!File.Exists(path))
        {
            Debug.LogError($"Archivo no encontrado: {path}");
            return preguntas;
        }
        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split('-');
                if (data.Length < 8) continue;
                PreguntasMultiples pregunta = new PreguntasMultiples(
                    data[0].Trim(), data[1].Trim(), data[2].Trim(), data[3].Trim(),
                    data[4].Trim(), data[5].Trim(), data[6].Trim(), data[7].Trim()
                );
                if ((allowEasy && pregunta.Dificultad.ToLower() == "facil") ||
                    (allowHard && pregunta.Dificultad.ToLower() == "dificil"))
                    preguntas.Add(pregunta);
            }
        }
        return preguntas;
    }

    public List<PreguntasAbiertas> LoadPreguntasAbiertas(string fileName, bool allowEasy, bool allowHard)
    {
        List<PreguntasAbiertas> preguntas = new List<PreguntasAbiertas>();
        fileName = CleanFileName(fileName);
        string path = Path.Combine(Application.dataPath, "Resources", "Files", fileName);
        if (!File.Exists(path))
        {
            Debug.LogError($"Archivo no encontrado: {path}");
            return preguntas;
        }
        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split('-');
                if (data.Length < 4) continue;
                PreguntasAbiertas pregunta = new PreguntasAbiertas(
                    data[0].Trim(), data[1].Trim(), data[2].Trim(), data[3].Trim()
                );
                if ((allowEasy && pregunta.Dificultad.ToLower() == "facil") ||
                    (allowHard && pregunta.Dificultad.ToLower() == "dificil"))
                    preguntas.Add(pregunta);
            }
        }
        return preguntas;
    }

    public List<PreguntasVerdaderoFalso> LoadPreguntasVerdaderoFalso(string fileName, bool allowEasy, bool allowHard)
    {
        List<PreguntasVerdaderoFalso> preguntas = new List<PreguntasVerdaderoFalso>();
        fileName = CleanFileName(fileName);
        string path = Path.Combine(Application.dataPath, "Resources", "Files", fileName);
        if (!File.Exists(path))
        {
            Debug.LogError($"Archivo no encontrado: {path}");
            return preguntas;
        }
        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split('-');
                if (data.Length < 4) continue;
                PreguntasVerdaderoFalso pregunta = new PreguntasVerdaderoFalso(
                    data[0].Trim(), data[1].Trim(), data[2].Trim(), data[3].Trim()
                );
                if ((allowEasy && pregunta.Dificultad.ToLower() == "facil") ||
                    (allowHard && pregunta.Dificultad.ToLower() == "dificil"))
                    preguntas.Add(pregunta);
            }
        }
        return preguntas;
    }
}






