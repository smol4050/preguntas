using models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Timer timer;
    public PreguntasMultiples currentQuestion;
    private int currentRound = 0;
    private List<PreguntasMultiples> pendingQuestions = new List<PreguntasMultiples>();
    private bool inputReceived;

    public static event System.Action OnRoundStart;
    public static event System.Action<bool, string> OnQuestionAnswered;

    void Start() => StartNewRound();

    void StartNewRound()
    {
        if (currentRound >= rounds.Count) return;
        LoadQuestionsForRound(rounds[currentRound]);
        StartCoroutine(StartRoundSequence(rounds[currentRound]));
    }

    void LoadQuestionsForRound(RoundConfig config)
    {
        pendingQuestions.Clear();
        foreach (string file in config.questionFiles)
        {
            List<PreguntasMultiples> qs = DataManager.Instance.LoadQuestions(
                file,
                config.allowEasy,
                config.allowHard
            );
            pendingQuestions.AddRange(qs);
        }
        ShuffleQuestions();
    }

    void ShuffleQuestions()
    {
        for (int i = pendingQuestions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            PreguntasMultiples temp = pendingQuestions[i];
            pendingQuestions[i] = pendingQuestions[j];
            pendingQuestions[j] = temp;
        }
    }

    IEnumerator StartRoundSequence(RoundConfig config)
    {
        OnRoundStart?.Invoke();
        timer.StartTimer(config.timeLimit, OnTimeUp);

        for (int i = 0; i < config.questionsPerRound; i++)
        {
            if (pendingQuestions.Count == 0) break;

            currentQuestion = GetNextQuestion();
            inputReceived = false;
            ShowRandomQuestionType();
            yield return new WaitUntil(() => inputReceived);
        }

        ProcessUnansweredQuestions();
        currentRound++;
        if (currentRound < rounds.Count) StartNewRound();
    }

    PreguntasMultiples GetNextQuestion()
    {
        PreguntasMultiples q = pendingQuestions[0];
        pendingQuestions.RemoveAt(0);
        return q;
    }

    void ShowRandomQuestionType()
    {
        QuestionType randomType = rounds[currentRound].allowedTypes[Random.Range(0, rounds[currentRound].allowedTypes.Count)];

        switch (randomType)
        {
            case QuestionType.Multiple:
                GetComponent<MultipleChoiceController>().ShowQuestion(currentQuestion);
                break;
            case QuestionType.Abierta:
                GetComponent<OpenQuestionController>().ShowQuestion(currentQuestion);
                break;
            case QuestionType.Booleana:
                GetComponent<BooleanQuestionController>().ShowQuestion(currentQuestion);
                break;
        }
    }

    void ProcessUnansweredQuestions()
    {
        foreach (var question in pendingQuestions)
        {
            RegisterAnswer(false, question.RespuestaCorrecta);
        }
        pendingQuestions.Clear();
    }

    void OnTimeUp() => ProcessUnansweredQuestions();

    public void RegisterAnswer(bool isCorrect, string correctAnswer)
    {
        inputReceived = true;
        OnQuestionAnswered?.Invoke(isCorrect, correctAnswer);
    }
}