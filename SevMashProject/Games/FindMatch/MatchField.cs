using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchField : MonoBehaviour
{
    public bool fieldIsFree;
    public bool fieldAnswerIsCorrect;
    public string rightAnswer;
    public string descriptionOfAnswer;
    public Image image;
    public Image frame;
    public Transform answerTransform;
    public Sprite DefoultFrame;
    public Sprite rightFrame;

    private void Start()
    {
        fieldIsFree = true;
    }
}
