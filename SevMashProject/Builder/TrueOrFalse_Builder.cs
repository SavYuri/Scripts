using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueOrFalse;
using TMPro;

public class TrueOrFalse_Builder : MonoBehaviour
{
    List<Question> questionsOfSession;
    private ScriptsManager scriptsManager;
    private bool _correctAnswerOfQuestion;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        scriptsManager = GameObject.FindGameObjectWithTag("ScriptsManager").
            GetComponent<ScriptsManager>();
    }

    public void BuildNewGame(int indexOfTopic)
    {
        InitNewQuestions(indexOfTopic);
        NextQuestion();
    }

    void InitNewQuestions(int indexOfTopic)
    {
        questionsOfSession = new List<Question>();
        questionsOfSession.AddRange(scriptsManager.trueOrFalse_Data.topic[indexOfTopic].question);
    }

    public void NextQuestion()
    {
        if (questionsOfSession.Count == 0)
        {
            scriptsManager.gameManager.GoToMainMenu();
            Debug.Log("Нет вопросов в списке");
            return;
        }

        int randomIndexOfQuestion = Random.Range(0, questionsOfSession.Count);

        scriptsManager.trueOrFalse_Controller.questionText.GetComponent<TMP_Text>().text =
            questionsOfSession[randomIndexOfQuestion].question;
        scriptsManager.trueOrFalse_Controller.answerText.GetComponent<TMP_Text>().text =
            questionsOfSession[randomIndexOfQuestion].descriptionOfAnswer;
        _correctAnswerOfQuestion = questionsOfSession[randomIndexOfQuestion].rightAnswer;


        questionsOfSession.RemoveAt(randomIndexOfQuestion);
    }

    public void AnswerBtn(bool answerBool)
    {
        if (answerBool == _correctAnswerOfQuestion) 
        {
            scriptsManager.trueOrFalse_Controller.LevelCompele(true);
            Debug.Log("Ответ правильный");
        }
        else
        {
            scriptsManager.trueOrFalse_Controller.LevelCompele(false);
            Debug.Log("Ответ неправильный");
        }
    }

}
