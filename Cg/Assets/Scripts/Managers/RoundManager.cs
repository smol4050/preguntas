using models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{

    public enum QuestionType { Multiple, Abierta, Booleana }

    [System.Serializable]
    public class RoundConfig
    {
        public List<QuestionType> allowedTypes;
        public List<string> questionFiles;
        public int questionsPerRound = 10;
        public bool allowEasy;
        public bool allowHard;
        public float timeLimit = 600f;
    }

    [SerializeField] private List<RoundConfig> rounds;
    [SerializeField] private MultipleChoiceController multipleChoiceController;
    [SerializeField] private OpenQuestionController openQuestionController;
    [SerializeField] private BooleanQuestionController booleanQuestionController;
    [SerializeField] private Timer timer;

    public PreguntaBase currentQuestion;
    private int currentRound = 0;
    private List<PreguntaBase> pendingQuestions = new List<PreguntaBase>();
    private bool inputReceived;

    
    private int roundCorrect = 0;
    private int roundTotal = 0;

    public static event System.Action OnRoundStart;
    public static event System.Action<bool, string> OnQuestionAnswered;

    void Start()
    {

        currentRound = 0;
        roundCorrect = 0;
        roundTotal = 0;
        StartNewRound();
    }

    void StartNewRound()
    {
        if (rounds == null || rounds.Count == 0)
        {
            Debug.LogError("La lista de rondas no está asignada en RoundManager.");
            return;
        }
        if (currentRound >= rounds.Count) return;
        // Actualiza el TextMeshPro de la ronda actual
        UIManager.Instance.SetRoundNumber(currentRound + 1);
        LoadQuestionsForRound(rounds[currentRound]);
        StartCoroutine(StartRoundSequence(rounds[currentRound]));
    }

    void LoadQuestionsForRound(RoundConfig config)
    {
        pendingQuestions.Clear();
        foreach (string file in config.questionFiles)
        {
            string trimmedFile = file.Trim();
            if (trimmedFile.Contains("Abiertas"))
                pendingQuestions.AddRange(DataManager.Instance.LoadPreguntasAbiertas(trimmedFile, config.allowEasy, config.allowHard));
            else if (trimmedFile.Contains("Falso_Verdadero"))
                pendingQuestions.AddRange(DataManager.Instance.LoadPreguntasVerdaderoFalso(trimmedFile, config.allowEasy, config.allowHard));
            else
                pendingQuestions.AddRange(DataManager.Instance.LoadPreguntasMultiples(trimmedFile, config.allowEasy, config.allowHard));
        }
        ShuffleQuestions();
    }

    void ShuffleQuestions()
    {
        for (int i = pendingQuestions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            PreguntaBase temp = pendingQuestions[i];
            pendingQuestions[i] = pendingQuestions[j];
            pendingQuestions[j] = temp;
        }
    }

    IEnumerator StartRoundSequence(RoundConfig config)
    {
        OnRoundStart?.Invoke();
        if (timer != null)
            timer.StartTimer(config.timeLimit, OnTimeUp);
        else
            Debug.LogError("La referencia del Timer es nula en RoundManager.");

        if (pendingQuestions.Count == 0)
        {
            Debug.LogError("No se cargaron preguntas para la ronda " + currentRound);
            yield break;
        }
        for (int i = 0; i < config.questionsPerRound; i++)
        {
            if (pendingQuestions.Count == 0) break;
            currentQuestion = pendingQuestions[0];
            pendingQuestions.RemoveAt(0);
            roundTotal++;
            inputReceived = false;
            ShowRandomQuestionType();
            UIManager.Instance.UpdateQuestionNumber(roundTotal, config.questionsPerRound);
            yield return new WaitUntil(() => inputReceived);
            
            yield return new WaitForSeconds(5f);
            UIManager.Instance.HideAllPanels();
        }
        yield return StartCoroutine(FinalizeRound());
    }

    void ShowRandomQuestionType()
    {
        if (currentQuestion is PreguntasMultiples)
        {
            if (multipleChoiceController != null)
                multipleChoiceController.ShowQuestion((PreguntasMultiples)currentQuestion);
            else
                Debug.LogError("Missing MultipleChoiceController reference in RoundManager");
        }
        else if (currentQuestion is PreguntasAbiertas)
        {
            if (openQuestionController != null)
                openQuestionController.ShowQuestion((PreguntasAbiertas)currentQuestion);
            else
                Debug.LogError("Missing OpenQuestionController reference in RoundManager");
        }
        else if (currentQuestion is PreguntasVerdaderoFalso)
        {
            if (booleanQuestionController != null)
                booleanQuestionController.ShowQuestion((PreguntasVerdaderoFalso)currentQuestion);
            else
                Debug.LogError("Missing BooleanQuestionController reference in RoundManager");
        }
        else
        {
            Debug.LogError("The current question type is not recognized: " + currentQuestion.GetType());
        }
    }

    public void RegisterAnswer(bool isCorrect, string correctAnswer)
    {
        inputReceived = true;
        if (isCorrect)
            roundCorrect++;
        OnQuestionAnswered?.Invoke(isCorrect, correctAnswer);
    }

    void ProcessUnansweredQuestions()
    {
        foreach (var question in pendingQuestions)
            RegisterAnswer(false, question.RespuestaCorrecta);
        pendingQuestions.Clear();
    }

    IEnumerator FinalizeRound()
    {
        UIManager.Instance.ShowFinalResult(roundCorrect >= 7, roundCorrect, roundTotal, currentRound);
        yield return new WaitForSeconds(10f);
        if (roundCorrect >= 7)
        {
            if (currentRound < rounds.Count - 1)
            {
                currentRound++;
                StartNewRound();
            }
            else
            {
                Debug.Log("Fin de todas las rondas");
                SceneManager.LoadScene(0);

            }
        }
        else
        {
            
            SceneManager.LoadScene(0);
        }
    }

    void OnTimeUp()
    {
        ProcessUnansweredQuestions();
    }
}
