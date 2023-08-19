using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpdate : MonoBehaviour
{
    float maxWidth;
    public Vector3 originalPos;

    void Awake(){
        maxWidth = gameObject.GetComponent<RectTransform>().rect.width;
        originalPos = gameObject.GetComponent<RectTransform>().localPosition;
    }

    public void updateHealthbar(float currentHP, float maxHP){
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(maxWidth*currentHP/maxHP, 25);
        gameObject.transform.localPosition = new Vector3(originalPos.x - (maxWidth  * (1f - currentHP/maxHP))/2, originalPos.y, 0);
    }
}
