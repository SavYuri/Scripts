using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    private bool _unityEditorMode;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _unityEditorMode = false;
        ActivateDisplay2();


    }
    private void ActivateDisplay2()
    {
#if UNITY_EDITOR

        _unityEditorMode = true;

#endif

        if (!_unityEditorMode)
        {
            Display.displays[1].Activate(2160, 3840, 60);
        }

    }
}
