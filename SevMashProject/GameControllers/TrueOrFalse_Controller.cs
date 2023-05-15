using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrueOrFalse_Controller : MonoBehaviour
{
    [SerializeField] GameObject btnHome;
    [SerializeField] GameObject btnNext;
    [SerializeField] GameObject btnTrue;
    [SerializeField] GameObject btnFalse;
    public GameObject questionText;
    public GameObject answerText;
    [SerializeField] GameObject headRightText;
    [SerializeField] GameObject rightText;
    [SerializeField] GameObject notRightText;

    private ScriptsManager scriptsManager;

    public int correctLevelIndex;
    [SerializeField] Image[] levelPoints;
    [SerializeField] Sprite levelCompleteSprite;
    [SerializeField] Sprite levelCorrectSprite;
    [SerializeField] Sprite levelNotCompleteSprite;


    private void Start()
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
        scriptsManager.trueOrFalse_Builder.BuildNewGame(indexOfTopic);
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
        questionText.SetActive(true);
        answerText.SetActive(false);
        headRightText.SetActive(false);
        rightText.SetActive(false);
        notRightText.SetActive(false);
        btnFalse.SetActive(true);
        btnTrue.SetActive(true);

        scriptsManager.gameManager.animatorRing.SetTrigger("Twist");
    }

    public void NextLevel()
    {
        if (correctLevelIndex == 5)
        {
            scriptsManager.gameManager.GoToWinGamePanel();
            return;
        }
        scriptsManager.trueOrFalse_Builder.NextQuestion();
        StartNewLevel();
    }

    public void LevelCompele(bool answerIsRight)
    {
        if (answerIsRight)
        {
            rightText.SetActive(true);
        }
        else
        {
            notRightText.SetActive(true);
        }

        btnFalse.SetActive(false);
        btnTrue.SetActive(false);
        btnHome.SetActive(true);
        btnNext.SetActive(true);
        questionText.SetActive(false);
        answerText.SetActive(true);
        headRightText.SetActive(true);
        correctLevelIndex++;
        ColorLevelsPoint();
    }


}
