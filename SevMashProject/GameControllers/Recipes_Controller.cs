using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipes_Controller : MonoBehaviour
{
    public GameObject answerText;
    public GameObject questionHeadText;
    public GameObject infoTextUp;
    public GameObject btnsOfIngredientsParent;
    public GameObject btnHome;
    public GameObject btnNext;
    public GameObject btnHelp;
    public GameObject answerImage;

    public int correctLevelIndex;
    [SerializeField] Image[] levelPoints;
    [SerializeField] Sprite levelCompleteSprite;
    [SerializeField] Sprite levelCorrectSprite;
    [SerializeField] Sprite levelNotCompleteSprite;

    private ScriptsManager scriptsManager;


    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        scriptsManager = GameObject.FindGameObjectWithTag("ScriptsManager").
            GetComponent<ScriptsManager>();
    }

    void Start()
    {
        Init();
    }

    void StartNewLevel()
    {
       answerText.SetActive(false);
        questionHeadText.SetActive(true);
        infoTextUp.SetActive(true);
        btnsOfIngredientsParent.SetActive(true);
        btnHome.SetActive(true);
        btnNext.SetActive(false);
        btnHelp.SetActive(true);
        infoTextUp.SetActive(true);
        answerImage.SetActive(false);
        scriptsManager.gameManager.animatorRing.SetTrigger("Twist");
    }

    public void NewGame(int indexOfTopic)
    {
        correctLevelIndex = 0;
        ColorLevelsPoint();
        StartNewLevel();
        scriptsManager.recipes_Builder.BuildNewGame(indexOfTopic);
        
    }
    void ColorLevelsPoint()
    {
        for (int i = 0; i < levelPoints.Length; i++)
        {
            if (i < correctLevelIndex)
            {
                levelPoints[i].sprite = levelCompleteSprite;
            }
            if (i == correctLevelIndex)
            {
                levelPoints[i].sprite = levelCorrectSprite;
            }
            if (i > correctLevelIndex)
            {
                levelPoints[i].sprite = levelNotCompleteSprite;
            }
        }
    }

    public void WinLevel()
    {
        correctLevelIndex++;

        ColorLevelsPoint();

        Debug.Log("Уровень пройден!");
        btnNext.SetActive(true);
        btnHelp.SetActive(false);
        //questionText.SetActive(false);
        answerText.SetActive(true);
        //questionHeadText.SetActive(false);
        infoTextUp.SetActive(false);
        btnsOfIngredientsParent.SetActive(false);
        answerImage.SetActive(true);
        //otlichnoText.SetActive(true);
        scriptsManager.collectTheWord_Builder.animatorWrongAnswr.SetTrigger("Right");
        scriptsManager.gameManager.animatorRing.SetTrigger("Twist");
    }

    public void NextLevel()
    {

        if (correctLevelIndex == 5)
        {
            scriptsManager.gameManager.GoToWinGamePanel();
            return;
        }

        StartCoroutine(scriptsManager.recipes_Builder.NextQuestion());
        StartNewLevel();
       // scriptsManager.collectTheWord_Builder.animatorWrongAnswr.SetTrigger("Defoult");
    }
}
