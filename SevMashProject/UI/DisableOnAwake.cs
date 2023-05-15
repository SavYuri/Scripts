using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnAwake : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.SetActive(false);
    }
}
