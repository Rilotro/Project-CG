using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EditDeckScipt : MonoBehaviour
{
    public GameObject DeckOverlay;
    GameObject DO;
    public GameObject Card;
    public GameObject CCES;//Card Count Edit System
    public int sizeRatio = 2;

    public void onClick(BaseEventData data){
        PointerEventData Pdata = (PointerEventData)data;
        if(Pdata.pointerId == -1){
            DO = Instantiate(DeckOverlay, new Vector3(0, 0, 0), Quaternion.identity);
            DO.transform.SetParent(gameObject.transform.parent, false);
            DO.name = "AddCardsOverlay";

            GameObject tempCard = Instantiate(Card, new Vector3(0, 0, 0), Quaternion.identity);
            tempCard.GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.SetParent(DO.transform, false);
            tempCard.GetComponent<CardInfo>().Give(Random.Range(1, 9));

            tempCard.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.transform.GetChild(0).GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.transform.GetChild(0).GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.GetChild(0).localPosition = new Vector3(tempCard.transform.GetChild(0).GetComponent<RectTransform>().rect.width/2 - tempCard.GetComponent<RectTransform>().rect.width/2, tempCard.GetComponent<RectTransform>().rect.height/2 - tempCard.transform.GetChild(0).GetComponent<RectTransform>().rect.height/2);

            tempCard.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.transform.GetChild(1).GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.transform.GetChild(1).GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.GetChild(1).localPosition = new Vector3(0, tempCard.transform.GetChild(1).GetComponent<RectTransform>().rect.height/2 - tempCard.GetComponent<RectTransform>().rect.height/2);

            tempCard.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.transform.GetChild(2).GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.transform.GetChild(2).GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.GetChild(2).localPosition = new Vector3((-1)*(tempCard.transform.GetChild(2).GetComponent<RectTransform>().rect.width/2 - tempCard.GetComponent<RectTransform>().rect.width/2), tempCard.GetComponent<RectTransform>().rect.height/2 - tempCard.transform.GetChild(2).GetComponent<RectTransform>().rect.height/2);

            GameObject tempText = Instantiate(CCES, new Vector3(0, 0, 0), Quaternion.identity);
            tempText.transform.GetChild(2).GetComponent<Text>().text = "0";
            tempText.transform.SetParent(tempCard.transform, false);
            tempText.name = "CCES";
            tempText.transform.localPosition = new Vector3(0, (-1)*(tempCard.GetComponent<RectTransform>().rect.height/2 + tempText.transform.GetChild(0).GetComponent<RectTransform>().rect.height/2), 0);
        }
    }
}
