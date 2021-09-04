using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float lerpAmount = 0.8f;

    private bool isBtnDown;

    private RectTransform rect;

    private Vector2 originalScale;
    private Vector2 criteriaScale;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalScale = rect.localScale;
        criteriaScale = originalScale * lerpAmount;
    }

    void Update()
    {
        if (isBtnDown)
        {
            rect.localScale = Vector3.Lerp(rect.localScale, criteriaScale, 0.3f);
        }
        else
        {
            rect.localScale = Vector3.Lerp(rect.localScale, originalScale, 0.3f);
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isBtnDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBtnDown = false;
    }

}
