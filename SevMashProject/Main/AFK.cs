using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFK : MonoBehaviour
{
    private ScriptsManager scriptsManager;
    private float _startTime;
    private float _timer;
    private bool _aFK_Activated;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        scriptsManager = GameObject.FindGameObjectWithTag("ScriptsManager").
            GetComponent<ScriptsManager>();
        _startTime = scriptsManager.settings.gameSettings.AFK_Time;


        Debug.Log("AFK Start Time: " + _startTime);
        _timer = _startTime;
    }

    private void Update()
    {
        TouchDetector();
        Timer();
    }

    void TouchDetector()
    {
        if (Input.anyKey)
        {
            _timer = _startTime;

            _aFK_Activated = false;
        }
    }


    private void Timer()
    {
        _timer -= Time.deltaTime;
        if (_aFK_Activated) return;
        if (_timer < 0)
        {
            _aFK_Activated = false;
            AFKCondition();
        }
    }

    private void AFKCondition()
    {
        _aFK_Activated = true;
    }
}
