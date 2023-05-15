using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;




public class InterpolationOfScreen : MonoBehaviour, IPointerDownHandler
{
    public GameObject touchLayer;
    public GameObject cursor;
    public PointerEventData _eventData;
    public float index;


    public void OnPointerDown(PointerEventData eventData)
    {
        
       // Debug.Log("Pointer down at position: " + eventData.position);

        Vector2 clickPosition = eventData.position; //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = clickPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform,
            eventData.position,
            Camera.main,
            out Vector2 localTouchPos
        );

        // Debug.Log(localTouchPos);
        // Use localTouchPos to interpolate touch on the TV screen
        Vector2 smallPos = new Vector2(eventData.position.x / index, eventData.position.y / index);
        cursor.transform.localPosition = smallPos;


    }

   

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging at position: " + eventData.position);
    }
}
