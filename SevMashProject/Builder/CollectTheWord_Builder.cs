using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollectTheWord;
using TMPro;
using UnityEngine.UI;

public class CollectTheWord_Builder : MonoBehaviour
{
    [SerializeField] Transform positionInstansiateOfLettersParent;
    [SerializeField] Transform positionForLettersParent;
    [SerializeField] Transform letterOfAnswer_Parent;
    [SerializeField] Transform fieldOfAnswer_Parent;
    [SerializeField] GameObject letterOfAnswer_Prefab;
    [SerializeField] GameObject fieldOfAnswer_Prefab;
    List <Question> questionsOfSession;
    public Animator animatorWrongAnswr;
    [SerializeField] Button helpBtn;
    
    private ScriptsManager scriptsManager;
    bool allFieldsNotFree;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        scriptsManager = GameObject.FindGameObjectWithTag("ScriptsManager").
            GetComponent<ScriptsManager>();
    }

    public void DisableDragOfLetters()
    {
        foreach (Transform letter in letterOfAnswer_Parent)
        {
            letter.GetComponent<LetterCTW>().enableDrag = false;
        }
    }

    public void BuildNewGame(int indexOfTopic)
    {
        InitNewQuestions(indexOfTopic);
        StartCoroutine(NextQuestion());
    }

    void ClearFields()
    {
        foreach (Transform child in letterOfAnswer_Parent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in fieldOfAnswer_Parent)
        {
            Destroy(child.gameObject);
        }
    }
  
    void InitNewQuestions(int indexOfTopic)
    {
        questionsOfSession = new List<Question>();
        questionsOfSession.AddRange(scriptsManager.collectTheWord_Data.topic[indexOfTopic].question);
    }

    public IEnumerator NextQuestion()
    {
       // yield return new WaitForSeconds(0.5f);
        ClearFields();
       
        if (questionsOfSession.Count == 0)
        {
            scriptsManager.gameManager.GoToMainMenu();
            Debug.Log("Нет вопросов в списке");
            yield break;
        }

        int randomIndexOfQuestion = Random.Range(0, questionsOfSession.Count);
        int countOfLettersInQuestion = questionsOfSession[randomIndexOfQuestion].rightAnswer.Length;

        

        for (int i = 0; i < countOfLettersInQuestion; i++)
        {
            
            //буква:
            GameObject letter = Instantiate(letterOfAnswer_Prefab, letterOfAnswer_Parent);
            //test
            //int randomInt = Random.Range(0, positionInstansiateOfLettersParent.childCount);
            letter.transform.position = positionInstansiateOfLettersParent.GetChild(i).transform.position;
            //

            LetterCTW letterCTW = letter.GetComponent<LetterCTW>();

            letterCTW.letterText.text =
                questionsOfSession[randomIndexOfQuestion].rightAnswer[i].ToString();

            letterCTW.answerLetter =
                questionsOfSession[randomIndexOfQuestion].rightAnswer[i];

            letterCTW.targetPosition = positionForLettersParent.GetChild(i).transform;
            letterCTW.defoultPosition = positionForLettersParent.GetChild(i).transform;
            //test:
            
            letterCTW.targetPosition = letterCTW.defoultPosition;
            letterCTW.isMoving = true; 



            //
            //letter.transform.position = positionForLettersParent.GetChild(i).transform.position;
            letter.transform.rotation = positionForLettersParent.GetChild(i).transform.rotation;

            //поле для буквы:
            GameObject field = Instantiate(fieldOfAnswer_Prefab, fieldOfAnswer_Parent);

            field.GetComponent<AnswerFieldCTW>().answerLetter = 
                questionsOfSession[randomIndexOfQuestion].rightAnswer[i];
        }

        //поля с текстом:
        CollectTheWordController collectTheWordController = scriptsManager.collectTheWordController;
        collectTheWordController.answerText.GetComponent<TextMeshProUGUI>().text =
            questionsOfSession[randomIndexOfQuestion].descriptionOfAnswer;
        collectTheWordController.questionText.GetComponent<TextMeshProUGUI>().text =
            questionsOfSession[randomIndexOfQuestion].question;

        //Картинка:
        if (questionsOfSession[randomIndexOfQuestion].nameOfImage != null)
        {
            Image image = scriptsManager.collectTheWordController.imageQuestion.GetComponent<Image>();
            string nameOfImage = questionsOfSession[randomIndexOfQuestion].nameOfImage;
            string path = PathData.collectTheWordImagePath;
            StartCoroutine(PhotoLoader.LoadImage(image, nameOfImage, path));
        }

        questionsOfSession.RemoveAt(randomIndexOfQuestion);
        yield break;
    }

    void WrongAnswer()
    {
        animatorWrongAnswr.SetTrigger("Wrong");
        scriptsManager.gameManager.animatorRing.SetTrigger("Wrong");
    }

   

    public void CheckWinCondition()
    {
        allFieldsNotFree = true;
        for (int i = 0; i < fieldOfAnswer_Parent.childCount; i++)
        {
            
            if (!fieldOfAnswer_Parent.GetChild(i).gameObject.
                GetComponent<AnswerFieldCTW>().fieldAnswerIsCorrect)
            {
                Debug.Log("Ответ неправильный!");
                
                for (int k = 0; k < fieldOfAnswer_Parent.childCount; k++)
                {
                    
                    if (fieldOfAnswer_Parent.GetChild(k).gameObject.
                GetComponent<AnswerFieldCTW>().fieldIsFree)
                    {
                        allFieldsNotFree = false;
                    }
                }
                if (allFieldsNotFree)
                {
                    WrongAnswer();
                    return;
                }
                return;
            }
        }
        scriptsManager.collectTheWordController.WinLevel();
    }

    public void HelpBtn()
    {
        StartCoroutine(Help());
    }

    IEnumerator Help()
    {
        helpBtn.interactable = false;

        LetterCTW letter = new LetterCTW ();
        AnswerFieldCTW answerField = new AnswerFieldCTW(); ;

        //находим правильную букву:
        for (int i = 0; i < letterOfAnswer_Parent.childCount; i++)
        {
            letter = letterOfAnswer_Parent.GetChild(i).GetComponent<LetterCTW>();
    
            if (letter.onFieldOfAnswerAndCorrect) continue;

            break;
        }

        //находим правильное поле для этой буквы:
        for ( int i = 0; i < fieldOfAnswer_Parent.childCount; i++)
        {
            answerField = fieldOfAnswer_Parent.GetChild(i).GetComponent<AnswerFieldCTW>();

            

            if (answerField.fieldAnswerIsCorrect)
            {
                continue;
            }

            if (!answerField.fieldIsFree)
            {
                if (letter.answerLetter == answerField.answerLetter)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            if (letter.answerLetter == answerField.answerLetter)
            {
                break;
            }
            else
            {
                continue;
            }


        }

        //перемещаем букву в поле:
        if (letter.targetPosition != letter.defoultPosition)
        {
            letter.targetPosition = letter.defoultPosition;
            letter.isMoving = true;
            letter.isDraging = false;
            yield return new WaitForSeconds(1);

        }
            letter.targetPosition = answerField.transform;
            letter.isMoving = true;
            letter.isDraging = false;
            yield return new WaitForSeconds(1);
            letter.initTargets();
        yield return new WaitForSeconds(0.5f);
        helpBtn.interactable = true;
        yield break;
    }
}
