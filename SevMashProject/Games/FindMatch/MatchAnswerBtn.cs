using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchAnswerBtn : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public string rightAnswer;
    private ScriptsManager scriptsManager;
    private RectTransform rectTransform;
    [HideInInspector] public Transform targetPosition;
    [HideInInspector] public Transform defoultPosition;
    [HideInInspector] public Transform answerPosition;
    private CanvasGroup canvasGroup;
    Canvas canvas;

    public float speed = 5.0f;
    public float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    public bool isMoving;
    public bool isDraging;
    public bool enableDrag;

    [HideInInspector] public MatchField matchField;
    public TextMeshProUGUI textOfAnswer;

    private void Start()
    {
        Init();
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        enableDrag = true;
        //targetPosition = defoultPosition;
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();

    }

    private void Init()
    {
        scriptsManager = GameObject.FindGameObjectWithTag("ScriptsManager").
            GetComponent<ScriptsManager>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (isMoving)
        {
            transform.position = Vector3.SmoothDamp(transform.position,
                targetPosition.position, ref velocity, smoothTime);
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!enableDrag) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        isMoving = false;
        isDraging = true;

        
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
        if (matchField != null && matchField.fieldIsFree)
        {
            targetPosition = answerPosition;
            matchField.fieldIsFree = false;
            StartCoroutine( CheckRightField());
        }
        else
        {
            targetPosition = defoultPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Cillider Enter");

        if (other.gameObject.GetComponent<MatchField>() != null &&
            other.gameObject.GetComponent<MatchField>().fieldIsFree)
        {
            matchField = other.gameObject.GetComponent<MatchField>();
            answerPosition = matchField.answerTransform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("Collider Exit");
        if (matchField != null)
        {
            matchField.fieldIsFree = true;
            matchField.fieldAnswerIsCorrect = false;
            matchField = null;
        }
    }

    public IEnumerator CheckRightField()
    {
        yield return new WaitForSeconds(1);

        if (rightAnswer == matchField.rightAnswer)
        {
            scriptsManager.findMatch_Controller.ShowDescription(matchField.descriptionOfAnswer);
            matchField.frame.sprite = matchField.rightFrame;
            matchField.GetComponent<Animator>().SetTrigger("Right");
            enableDrag = false;
            matchField.fieldAnswerIsCorrect = true;
            scriptsManager.findMatch_Builder.CheckWinCondition();
        }
        else
        {
            targetPosition = defoultPosition;
            isMoving = true;
            isDraging = false;
            matchField.frame.sprite = matchField.DefoultFrame;
            matchField.GetComponent<Animator>().SetTrigger("Wrong");
        }

        yield break;
    }
}
