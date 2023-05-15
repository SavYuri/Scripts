using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindMatch_Controller : MonoBehaviour
{
    private ScriptsManager scriptsManager;
    public int correctLevelIndex;
    [SerializeField] Image[] levelPoints;
    [SerializeField] Sprite levelCompleteSprite;
    [SerializeField] Sprite levelCorrectSprite;
    [SerializeField] Sprite levelNotCompleteSprite;
    [SerializeField] GameObject btnHome;
    [SerializeField] GameObject btnNext;
    [SerializeField] GameObject btnHelp;
    [SerializeField] GameObject fieldsMatch;
    [SerializeField] GameObject answerBtns;
    public GameObject descriptionText;
    [SerializeField] GameObject otlichnoText;
    [SerializeField] GameObject infoText;



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
        scriptsManager.findMatch_Builder.BuildNewGame(indexOfTopic);
        StartNewLevel();
    }

    public void NextLevel()
    {
        if (correctLevelIndex == 5)
        {
            scriptsManager.gameManager.GoToWinGamePanel();
            return;
        }
        StartCoroutine(scriptsManager.findMatch_Builder.NextQuestion());
        StartNewLevel();
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

    void StartNewLevel()
    {
        btnHome.SetActive(true);
        btnNext.SetActive(false);
        btnHelp.SetActive(true);
        fieldsMatch.SetActive(true);
        answerBtns.SetActive(true);
        descriptionText.SetActive(false);
        otlichnoText.SetActive(false);
        infoText.SetActive(true);
        scriptsManager.gameManager.animatorRing.SetTrigger("Twist");
    }

    public void ShowDescription(string info)
    {
        descriptionText.SetActive(false);
        descriptionText.SetActive(true);
        infoText.SetActive(false);
        descriptionText.GetComponent<TextMeshProUGUI>().text = info;
    }

    public void WinLevel()
    {
        correctLevelIndex++;
        ColorLevelsPoint();
        otlichnoText.SetActive(true);
        btnHelp.SetActive(false);
        btnNext.SetActive(true);
    }
}
