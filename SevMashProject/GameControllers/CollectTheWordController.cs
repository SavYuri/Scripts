using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectTheWordController : MonoBehaviour
{
    [Header("Objects:")]
    public GameObject answerField;
    public GameObject answerFieldFinal;
    public GameObject imageQuestion;
    [SerializeField] GameObject btnHome;
    [SerializeField] GameObject btnNext;
    [SerializeField] GameObject btnHelp;
    public GameObject questionText;
    public GameObject answerText;
    public GameObject otlichnoText;
    [SerializeField] Image [] levelPoints;
    public int correctLevelIndex;
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

    public void NewGame(int indexOfTopic)
    {
        correctLevelIndex = 0;
        ColorLevelsPoint();
        scriptsManager.collectTheWord_Builder.BuildNewGame(indexOfTopic);
        StartNewLevel();
    }

    void StartNewLevel()
    {
        answerField.SetActive(true);
        answerFieldFinal.SetActive(false);
        imageQuestion.SetActive(true);
        btnHome.SetActive(true);
        btnNext.SetActive(false);
        btnHelp.SetActive(true);
        questionText.SetActive(true);
        answerText.SetActive(false);
        otlichnoText.SetActive(false);
        scriptsManager.gameManager.animatorRing.SetTrigger("Twist");
    }

    public void NextLevel()
    {

        if (correctLevelIndex == 5)
        {
            scriptsManager.gameManager.GoToWinGamePanel();
            return;
        }

        StartCoroutine( scriptsManager.collectTheWord_Builder.NextQuestion());
        StartNewLevel();
        scriptsManager.collectTheWord_Builder.animatorWrongAnswr.SetTrigger("Defoult");
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
        questionText.SetActive(false);
        answerText.SetActive(true);
        otlichnoText.SetActive(true);
        scriptsManager.collectTheWord_Builder.animatorWrongAnswr.SetTrigger("Right");
        scriptsManager.gameManager.animatorRing.SetTrigger("Twist");
        scriptsManager.collectTheWord_Builder.DisableDragOfLetters();
    }
}
