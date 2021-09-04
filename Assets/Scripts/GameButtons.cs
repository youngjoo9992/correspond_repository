using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtons : MonoBehaviour
{
    public Vector2 buttonDestination;

    void Update()
    {
        GetComponent<RectTransform>().localPosition = Vector3.Lerp(GetComponent<RectTransform>().localPosition, buttonDestination, 0.3f);
    }
}
