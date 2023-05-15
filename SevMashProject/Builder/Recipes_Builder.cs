using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipes;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class Recipes_Builder : MonoBehaviour
{
    List<Question> questionsOfSession;
    [SerializeField] GameObject ingredientsBtnsParent;

    private ScriptsManager scriptsManager;
    bool allFieldsNotFree;
    List<string> namesOfIgredients;
    [SerializeField] Button helpBtn;
    [SerializeField] Animator animator;
    [SerializeField] Animator questionHeadAnimator;

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

    void ReadAllNamesOfIngredients()
    {
        namesOfIgredients = new List<string>();
        string streamingAssetsPath =
           Path.Combine(Application.streamingAssetsPath, "PhotoQuestions", "Recipes", "QuestionPhotos");

        Debug.Log(streamingAssetsPath);

        string searchPattern = "*.png";
        string[] fileNames = Directory.GetFiles(streamingAssetsPath, searchPattern, SearchOption.AllDirectories);

        foreach (string fileName in fileNames)
        {
            string file = Path.GetFileName(fileName);
            // Debug.Log("File name: " + file);
            string ingredient = file.Replace(".png", "");
            // Debug.Log("File name: " + ingredient);
            namesOfIgredients.Add(ingredient);
        }
    }

    void InitNewQuestions(int indexOfTopic)
    {
        questionsOfSession = new List<Question>();
        questionsOfSession.AddRange(scriptsManager.recipes_Data.topic[indexOfTopic].question);
       // ReadAllNamesOfIngredients();
    }

    public IEnumerator NextQuestion()
    {
        questionHeadAnimator.SetTrigger("Defoult");

        if (questionsOfSession.Count == 0)
        {
            scriptsManager.gameManager.GoToMainMenu();
            Debug.Log("Ќет вопросов в списке");
            yield break;
        }

        ReadAllNamesOfIngredients();

        int randomIndexOfQuestion = Random.Range(0, questionsOfSession.Count);

        //пол€ с текстом:
        scriptsManager.recipes_Controller.questionHeadText.GetComponent<TextMeshProUGUI>().text =
            questionsOfSession[randomIndexOfQuestion].question;
        scriptsManager.recipes_Controller.answerText.GetComponent<TextMeshProUGUI>().text =
            questionsOfSession[randomIndexOfQuestion].descriptionOfAnswer;

        //  артинка ответа:
        Image image = scriptsManager.recipes_Controller.answerImage.GetComponent<Image>();
        string nameOfImage = questionsOfSession[randomIndexOfQuestion].nameOfAnswerImage;
        string path = PathData.recipesAnswerPhotosPath;
        StartCoroutine(PhotoLoader.LoadImage(image, nameOfImage, path));

        //инициализируем кнопки ингридиентов
        List<int> indexOfbtns = new List<int>() { 0, 1, 2, 3, 4, 5 };
        for (int i = 0; i < 6; i++)
        {
            int randomIndexOfBtn = Random.Range(0, indexOfbtns.Count);
            Ingredient btnIngredient =
                ingredientsBtnsParent.transform.GetChild(indexOfbtns[randomIndexOfBtn]).GetComponent<Ingredient>();
            indexOfbtns.RemoveAt(randomIndexOfBtn);


            Image imageIngredient = btnIngredient.ingredientImage;
            string nameOfImageIngredient;
            string pathOfImageIngredient =
                    Path.Combine(Application.streamingAssetsPath, PathData.recipesQuestionPhotosPath);
            //если есть ингредиент:
            if (questionsOfSession[randomIndexOfQuestion].nameOfQuestionImage.Length > i)
            {
                nameOfImageIngredient = questionsOfSession[randomIndexOfQuestion].nameOfQuestionImage[i];

                StartCoroutine(PhotoLoader.LoadImage(imageIngredient, nameOfImageIngredient, pathOfImageIngredient));

                btnIngredient.ingredientStr = nameOfImageIngredient;
                btnIngredient.itIsRightIngredient = true;
                btnIngredient.NewBtn();
                namesOfIgredients.Remove(nameOfImageIngredient);
            }
            else //если нет ингредиента:
            {
                nameOfImageIngredient = namesOfIgredients[0];
                namesOfIgredients.RemoveAt(0);

                StartCoroutine(PhotoLoader.LoadImage(imageIngredient, nameOfImageIngredient, pathOfImageIngredient));

                btnIngredient.ingredientStr = "";
                btnIngredient.itIsRightIngredient = false;
                btnIngredient.NewBtn();
            }

        }
        questionsOfSession.RemoveAt(randomIndexOfQuestion);
        yield break;
    }

    public void CheckWinCondition()
    {
        for (int i = 0; i < ingredientsBtnsParent.transform.childCount; i++)
        {
            Ingredient ingredient =
                ingredientsBtnsParent.transform.GetChild(i).GetComponent<Ingredient>();

            if (!ingredient.btnInRightAnswerState)
            {
                animator.SetTrigger("Wrong");
                scriptsManager.gameManager.animatorRing.SetTrigger("Wrong");
                return;
            }
        }
        questionHeadAnimator.SetTrigger("Win");

        scriptsManager.recipes_Controller.WinLevel();
        
        Debug.Log("ќтвет правильный");
    }

    public void HelpBtn()
    {
        StartCoroutine(Help());
    }

    IEnumerator Help()
    {
        helpBtn.interactable = false;
        for (int i = 0; i < ingredientsBtnsParent.transform.childCount; i++)
        {
            Ingredient ingredient = 
                ingredientsBtnsParent.transform.GetChild(i).GetComponent<Ingredient>();

            if (ingredient.btnInRightAnswerState)
            {
                continue;
            }
            else
            {
                StartCoroutine(ingredient.Click());
                yield return new WaitForSeconds(1);
               // CheckWinCondition();
                helpBtn.interactable = true;
                yield break;
            }
        }
        helpBtn.interactable = true;
        yield break;
    }
}
