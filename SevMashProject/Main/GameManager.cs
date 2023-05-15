using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScriptsManager scriptsManager;
    public int indexOfTopic;
    public Animator animatorRing;
    public Animator panelsAnimator;


    private void Start()
    {
        Init();
        GoToMainMenu();
    }

    private void Init()
    {
        scriptsManager = GameObject.FindGameObjectWithTag("ScriptsManager").
            GetComponent<ScriptsManager>();
    }

    public void GoToWinGamePanel()
    {
        StartCoroutine(GotoWinPanel());
    }

    IEnumerator GotoWinPanel()
    {
        panelsAnimator.SetTrigger("GamesOut");
        yield return new WaitForSeconds(0.5f);
        scriptsManager.objectsData.gamesPanel.SetActive(false);
        scriptsManager.objectsData.winPanel.SetActive(true);
       
        yield break;
    }

    public void GoToMainMenu()
    {
        //Game Panels:
        scriptsManager.objectsData.collectTheWordPanel.SetActive(false);
        scriptsManager.objectsData.trueOrFalsePanel.SetActive(false);
        scriptsManager.objectsData.puzzlePanel.SetActive(false);
        scriptsManager.objectsData.findMatchPanel.SetActive(false);
        scriptsManager.objectsData.recipesPanel.SetActive(false);

        scriptsManager.objectsData.mainMenuPanel.SetActive(true);
        scriptsManager.objectsData.gameTypeMenu.SetActive(false);
        scriptsManager.objectsData.winPanel.SetActive(false);
        animatorRing.SetTrigger("Twist");
        panelsAnimator.SetTrigger("Defoult");
    }

    public void ChooseTopic(int indexTopic)
    {
        indexOfTopic = indexTopic;
        scriptsManager.objectsData.mainMenuPanel.SetActive(false);
        scriptsManager.objectsData.gameTypeMenu.SetActive(true);
    }

    // 0 - Collect The Word; 1 - True Or False; 3 - Puzzle;
    // 4 - Find Match; 5 - Receipt
    public void StartGame(int indexTypeOfGame)
    {
        scriptsManager.objectsData.gameTypeMenu.SetActive(false);
        scriptsManager.objectsData.gamesPanel.SetActive(true);
        



        if (indexTypeOfGame == 0)
        {
            scriptsManager.objectsData.collectTheWordPanel.SetActive(true);
            scriptsManager.collectTheWordController.NewGame(indexOfTopic);
        }
        else if(indexTypeOfGame == 1)
        {
            scriptsManager.objectsData.trueOrFalsePanel.SetActive(true);
            scriptsManager.trueOrFalse_Controller.NewGame(indexOfTopic);
        }
        else if (indexTypeOfGame == 2)
        {
            scriptsManager.objectsData.recipesPanel.SetActive(true);
            scriptsManager.recipes_Controller.NewGame(indexOfTopic);
        }
        else if (indexTypeOfGame == 3)
        {
            scriptsManager.objectsData.findMatchPanel.SetActive(true);
            scriptsManager.findMatch_Controller.NewGame(indexOfTopic);
        }
    }
}
