using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardPreview : MonoBehaviour
{
    public void Preview(BaseEventData data)
    {
        PointerEventData Pdata = (PointerEventData)data;
        if((gameObject.transform.parent.gameObject.name == "PlayerHand")&&(Pdata.pointerId == -3)){
            GameObject cardPreview = Instantiate(gameObject, new Vector3(413, 171, 0), Quaternion.identity);
            cardPreview.transform.SetParent(gameObject.transform.parent.parent, false);
            RectTransform rect = cardPreview.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(279.95f, 394.49f);

            rect = cardPreview.transform.GetChild(0).GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(42, 42);
            cardPreview.transform.GetChild(0).localPosition = new Vector3(-118.97f, 176.24f, 0);

            rect = cardPreview.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(42, 42);
            cardPreview.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().fontSize = 25;

            rect = cardPreview.transform.GetChild(1).GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(250, 180);
            cardPreview.transform.GetChild(1).localPosition = new Vector3(0, -90, 0);
            
            rect = cardPreview.transform.GetChild(2).GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(42, 42);
            cardPreview.transform.GetChild(2).localPosition = new Vector3(118.98f, 176.24f, 0);

            rect = cardPreview.transform.GetChild(2).transform.GetChild(0).GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(42, 42);
            cardPreview.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().fontSize = 25;

        }else if(gameObject.transform.parent.gameObject.name != "PlayerHand"){
            Destroy(gameObject);
        }
    }

}
