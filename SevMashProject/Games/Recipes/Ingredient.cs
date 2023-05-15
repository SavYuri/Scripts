using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    public string ingredientStr;
    public Image ingredientImage;
    public Image backgroundImage;
    [SerializeField] Color selectedColor;
    [SerializeField] Color unselectedColor;
    public bool btnInRightAnswerState;
    public bool itIsRightIngredient;
    public bool btnIsSelected;
    Button btn;
    Animator _animator;
    bool newBtn;

    private ScriptsManager scriptsManager;
    

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        scriptsManager = GameObject.FindGameObjectWithTag("ScriptsManager").
            GetComponent<ScriptsManager>();
    }


    private void Awake()
    {
        btn = GetComponent<Button>();
        btnIsSelected = true;
        btn.onClick.AddListener(() => StartCoroutine(Click()));
        _animator = GetComponent<Animator>();
        
    }

    
    public void NewBtn()
    {
        if (!transform.parent.gameObject.activeSelf) 
        {
            transform.parent.gameObject.SetActive(true);
        }
        newBtn = true;
        btnIsSelected = true;
        StartCoroutine(Click());
        _animator = GetComponent<Animator>();
    }

    public IEnumerator Click()
    {
        btnIsSelected = !btnIsSelected;
        _animator.SetTrigger("Click");
       // yield return new WaitForSeconds(0.6f);

        if (btnIsSelected)
        {
            backgroundImage.color = selectedColor;
        }
        else
        {
            backgroundImage.color = unselectedColor;
        }
        CheckAnswerState();
        yield return new WaitForSeconds(1);
        if (!newBtn)
        {
            scriptsManager.recipes_Builder.CheckWinCondition();
        }
        newBtn = false;
        
        yield break;
    }
    void CheckAnswerState()
    {
        if (itIsRightIngredient && btnIsSelected)
        {
            btnInRightAnswerState = true;
        }
        else if (!itIsRightIngredient && !btnIsSelected)
        {
            btnInRightAnswerState = true;
        }
        else
        {
            btnInRightAnswerState = false;
        }
    }


}
