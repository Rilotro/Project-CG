using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckScript : MonoBehaviour
{
    public GameObject Card;
    static public List<GameObject> DeckCards = new List<GameObject>();
    public GameObject DO;
    public GameObject DeckOverlay;
    //public int sizeRatio = 2;

    void Awake(){
        DO = Instantiate(DeckOverlay, new Vector3(0, 0, 0), Quaternion.identity);
        DO.transform.SetParent(gameObject.transform.parent, false);
        DO.transform.localPosition = new Vector3(0, 0, 0);
        DO.transform.SetSiblingIndex(0);
        DO.name = "DeckOverlay";
        GameObject tempCard;
        for(int i = 0; i<40; i++){
            tempCard = Instantiate(Card, new Vector3(-500, 0, 0), Quaternion.identity);
            //tempCard.GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.SetParent(DO.transform, false);
            tempCard.GetComponent<CardInfo>().Give(Random.Range(1, 9));
            DeckCards.Add(tempCard);

            /*tempCard.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.transform.GetChild(0).GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.transform.GetChild(0).GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.GetChild(0).localPosition = new Vector3(tempCard.transform.GetChild(0).GetComponent<RectTransform>().rect.width/2 - tempCard.GetComponent<RectTransform>().rect.width/2, tempCard.GetComponent<RectTransform>().rect.height/2 - tempCard.transform.GetChild(0).GetComponent<RectTransform>().rect.height/2);

            tempCard.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.transform.GetChild(1).GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.transform.GetChild(1).GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.GetChild(1).localPosition = new Vector3(0, tempCard.transform.GetChild(1).GetComponent<RectTransform>().rect.height/2 - tempCard.GetComponent<RectTransform>().rect.height/2);

            tempCard.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.transform.GetChild(2).GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.transform.GetChild(2).GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(tempCard.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().rect.width*sizeRatio, tempCard.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().rect.height*sizeRatio);
            tempCard.transform.GetChild(2).localPosition = new Vector3((-1)*(tempCard.transform.GetChild(2).GetComponent<RectTransform>().rect.width/2 - tempCard.GetComponent<RectTransform>().rect.width/2), tempCard.GetComponent<RectTransform>().rect.height/2 - tempCard.transform.GetChild(2).GetComponent<RectTransform>().rect.height/2);*/
        }
    }

    public void onClick(BaseEventData data){
        PointerEventData Pdata = (PointerEventData)data;
        if((Pdata.pointerId == -1)&&(DO.transform.GetSiblingIndex() <= 1)){
            DO.transform.SetSiblingIndex(gameObject.transform.parent.childCount-1);
        }
        
    }
}
