using Recipes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScriptsManager : MonoBehaviour
{
    public GameManager gameManager;
    public ObjectsData objectsData;
    public Settings settings;
    //public PhotoLoader photoLoader;

    //Collect The Word
    public CollectTheWordController collectTheWordController;
    public CollectTheWord.CollectTheWord_Data collectTheWord_Data;
    public CollectTheWord_Builder collectTheWord_Builder;
    //True Or False
    public TrueOrFalse_Controller trueOrFalse_Controller;
    public TrueOrFalse.TrueOrFalse_Data trueOrFalse_Data;
    public TrueOrFalse_Builder trueOrFalse_Builder;
    //Recipes
    public Recipes_Controller recipes_Controller;
    public Recipes_Builder recipes_Builder;
    public Recipes_Data recipes_Data;
    //Find Match
    public FindMatch_Controller findMatch_Controller;
    public FindMatch_Builder findMatch_Builder;
    public FindMatch.FindMatch_Data findMatch_Data;

}
