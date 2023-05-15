using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FindMatch;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class FindMatch_Builder : MonoBehaviour
{
    List<Question> questionsOfSession;

    private ScriptsManager scriptsManager;

    [SerializeField] Transform fieldsMatchParent;
    [SerializeField] Transform answerBtnsParent;
    [SerializeField] Transform defoultPositionParent;
    [SerializeField] Button helpButton;


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
        StartCoroutine(NextQuestion());
    }

    void InitNewQuestions(int indexOfTopic)
    {
        questionsOfSession = new List<Question>();
        questionsOfSession.AddRange(scriptsManager.findMatch_Data.topic[indexOfTopic].question);
    }

    public IEnumerator NextQuestion()
    {

        if (questionsOfSession.Count == 0)
        {
            scriptsManager.gameManager.GoToMainMenu();
            Debug.Log("Нет вопросов в списке");
            yield break;
        }

        List<int> indexOfAnswerBtn = new List<int>() { 0, 1, 2, 3, 4, 5 };
        List<Question> questionsForWrongAnswer = new List<Question>();


        for (int i = 0; i < 6; i++)
        {
            int randomIndexOfQuestion = Random.Range(0, questionsOfSession.Count);
            int randomIndex = Random.Range(0, indexOfAnswerBtn.Count);
            int randomIndexOfBtn = indexOfAnswerBtn[randomIndex];
            indexOfAnswerBtn.RemoveAt(randomIndex);
            MatchAnswerBtn answerBtn = answerBtnsParent.GetChild(randomIndexOfBtn).gameObject.GetComponent<MatchAnswerBtn>();
            answerBtn.defoultPosition = defoultPositionParent.GetChild(randomIndexOfBtn);
            answerBtn.targetPosition = defoultPositionParent.GetChild(randomIndexOfBtn);
            answerBtn.enableDrag = true;
            answerBtn.transform.position = defoultPositionParent.GetChild(randomIndexOfBtn).position;

            if (i < 3)
            {
                //поле для ответа:
                MatchField answerField = fieldsMatchParent.GetChild(i).gameObject.GetComponent<MatchField>();
                answerField.rightAnswer = questionsOfSession[randomIndexOfQuestion].rightAnswer;
                answerField.descriptionOfAnswer = questionsOfSession[randomIndexOfQuestion].descriptionOfAnswer;
                answerField.fieldIsFree = true;
                answerField.frame.sprite = answerField.DefoultFrame;
                //картинка:
                Image image = answerField.image;
                string nameOfImage = questionsOfSession[randomIndexOfQuestion].nameOfImage;
                string path = PathData.findMatchImagesPath;
                StartCoroutine(PhotoLoader.LoadImage(image, nameOfImage, path));

                //кнопка для ответа:
                answerBtn.rightAnswer = questionsOfSession[randomIndexOfQuestion].rightAnswer;
                answerBtn.textOfAnswer.text = questionsOfSession[randomIndexOfQuestion].rightAnswer;

                questionsOfSession.RemoveAt(randomIndexOfQuestion);

                questionsForWrongAnswer = new List<Question>();
                questionsForWrongAnswer.AddRange(questionsOfSession);
                Debug.Log("Ответ добавлен");
            }
            else
            {
                int randomIndexOfQuestionForWrongAnswer = Random.Range(0, questionsForWrongAnswer.Count);
                //кнопка для ответа:
                answerBtn.rightAnswer = questionsForWrongAnswer[randomIndexOfQuestionForWrongAnswer].rightAnswer;
                answerBtn.textOfAnswer.text = questionsForWrongAnswer[randomIndexOfQuestionForWrongAnswer].rightAnswer;
                questionsForWrongAnswer.RemoveAt(randomIndexOfQuestionForWrongAnswer);
                Debug.Log("Ответ добавлен");
            }

        }

        yield break;
    }

    public void CheckWinCondition()
    {
        for (int i = 0; i < fieldsMatchParent.childCount; i++)
        {
            MatchField fieldAnswer = fieldsMatchParent.GetChild(i).GetComponent<MatchField>();
            if (fieldAnswer.fieldAnswerIsCorrect)
            {
                continue;
            }
            else
            {
                Debug.Log("Ответ неправильный");
                return;

            }

        }
        scriptsManager.findMatch_Controller.WinLevel();

    }

    public void HelpBtn()
    {
        StartCoroutine(Help());
    }

    IEnumerator Help()
    {
        //находим пустое поле для ответа:
        for (int i = 0; i < fieldsMatchParent.childCount; i++)
        {
            MatchField fieldOfAnswer = fieldsMatchParent.GetChild(i).GetComponent<MatchField>();

            if (fieldOfAnswer.fieldIsFree)
            {
                for(int x = 0; x < answerBtnsParent.childCount; x++)
                {
                    MatchAnswerBtn answerBtn = answerBtnsParent.GetChild(x).GetComponent<MatchAnswerBtn>();
                    if (answerBtn.rightAnswer == fieldOfAnswer.rightAnswer)
                    {
                        helpButton.interactable =false;
                        answerBtn.targetPosition = fieldOfAnswer.answerTransform;
                        fieldOfAnswer.fieldIsFree = false;
                        answerBtn.isMoving = true;
                        answerBtn.isDraging = false;
                        answerBtn.matchField = fieldOfAnswer;
                        yield return new WaitForSeconds(1f);

                        StartCoroutine(answerBtn.CheckRightField());
                        yield return new WaitForSeconds(1.5f);
                        helpButton.interactable = true;
                        yield break;
                    }
                }
            }
        }
        yield break;
    }
}
