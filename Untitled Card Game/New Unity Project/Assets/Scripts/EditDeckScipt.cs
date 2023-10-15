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
            DO.GetComponent<GridLayoutGroup>().spacing = new Vector2(50, 25);

            for(int i = 1; i < 9; i++){
                GameObject tempCard = Instantiate(Card, new Vector3(0, 0, 0), Quaternion.identity);
                tempCard.transform.SetParent(DO.transform, false);
                tempCard.GetComponent<CardInfo>().Give(i);

                GameObject tempText = Instantiate(CCES, new Vector3(0, 0, 0), Quaternion.identity);
                tempText.transform.GetChild(2).GetComponent<Text>().text = "0";
                tempText.transform.SetParent(tempCard.transform, false);
                tempText.name = "CCES";
                tempText.transform.localPosition = new Vector3(0, (-1)*(tempCard.GetComponent<RectTransform>().rect.height/2 + tempText.transform.GetChild(0).GetComponent<RectTransform>().rect.height/2), 0);
            }
        }
    }
}
