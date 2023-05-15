using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class LetterCTW : MonoBehaviour, IDragHandler, IEndDragHandler
{
    //public int indexRightAnswer;
    public char answerLetter;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Canvas canvas;
    [HideInInspector] public Transform targetPosition;
    [HideInInspector] public Transform defoultPosition;
    [HideInInspector] public Transform answerPosition;
    AnswerFieldCTW answerFieldCTW;
    

    public float speed = 5.0f;
    public float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    public bool isMoving;
    public bool isDraging;
    public bool enableDrag;
    public bool onFieldOfAnswerAndCorrect;
    public bool onFieldOfAnswerAnd_NOT_Correct;

    public TextMeshProUGUI letterText;

    private ScriptsManager scriptsManager;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        enableDrag = true;
        //targetPosition = defoultPosition;
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();

    }

    


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        scriptsManager = GameObject.FindGameObjectWithTag("ScriptsManager").
            GetComponent<ScriptsManager>();
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        if (isMoving)
        {
            transform.position = Vector3.SmoothDamp(transform.position,
                targetPosition.position, ref velocity, smoothTime);
        }
       
    }

    void Rotate()
    {
        if (isDraging)
        {
            gameObject.transform.rotation = 
                Quaternion.Slerp(gameObject.transform.rotation, new Quaternion(0f,0f,0f,0f), 0.06f); 
        }
        else
        {
            gameObject.transform.rotation = 
                Quaternion.Slerp(gameObject.transform.rotation, targetPosition.rotation, 0.005f);
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!enableDrag) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        isMoving = false;
        isDraging = true;

        onFieldOfAnswerAndCorrect = false;
        onFieldOfAnswerAnd_NOT_Correct = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!enableDrag) return;

        initTargets();
        isMoving = true;
        isDraging = false;
    }

    public void initTargets()
    {
      if (answerFieldCTW != null && answerFieldCTW.fieldIsFree)
      {
            targetPosition = answerPosition;
            answerFieldCTW.fieldIsFree = false;
            if(answerFieldCTW.answerLetter == answerLetter)
            {
                answerFieldCTW.fieldAnswerIsCorrect = true;
                onFieldOfAnswerAndCorrect = true;
            }
            else
            {
                onFieldOfAnswerAnd_NOT_Correct = true;
            }
            Invoke("CheckWinCondition", 0.5f);
      }
      else
      {
           targetPosition = defoultPosition;
      }
    }

    void CheckWinCondition()
    {
        scriptsManager.collectTheWord_Builder.CheckWinCondition();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Cillider Enter");
        
        if (other.gameObject.GetComponent<AnswerFieldCTW>() != null && 
            other.gameObject.GetComponent<AnswerFieldCTW>().fieldIsFree)
        {
            answerFieldCTW = other.gameObject.GetComponent<AnswerFieldCTW>();
            answerPosition = other.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
       // Debug.Log("Collider Exit");
        if (answerFieldCTW != null)
        {
            answerFieldCTW.fieldIsFree = true;
            answerFieldCTW.fieldAnswerIsCorrect = false;
            answerFieldCTW = null;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
       //Debug.Log("Object is in the collider!");
    }
}
