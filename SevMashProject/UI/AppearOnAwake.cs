using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnAwake : MonoBehaviour
{
    [SerializeField] float speed;
    private CanvasGroup canvasGroup;
    public bool startAppear;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
    void Update()
    {
        if (canvasGroup.alpha >= 1 || !startAppear)
        {
            startAppear = false;
            return;
        }

        canvasGroup.alpha += Time.deltaTime * speed;
    }

    void OnEnable()
    {
        startAppear = true;
        canvasGroup.alpha = 0;
    }
}
