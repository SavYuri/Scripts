using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveTrueOnAwake : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.SetActive(true);
    }
}
