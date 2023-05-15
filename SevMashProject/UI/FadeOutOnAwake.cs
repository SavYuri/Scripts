using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeOutOnAwake : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [SerializeField] float speed; 

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

   

    private void Update()
    {
        canvasGroup.alpha += speed * Time.deltaTime;
    }

    private void OnEnable()
    {
        canvasGroup.alpha = 0;
    }

    private void OnDisable()
    {
        canvasGroup.alpha = 0;
    }
}
