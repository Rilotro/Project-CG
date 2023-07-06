using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckScript : MonoBehaviour
{
    public GameObject Card;
    static public List<GameObject> DeckCards = new List<GameObject>();
    static public GameObject DO;
    public GameObject DeckOverlay;

    void Awake(){
        DO = Instantiate(DeckOverlay, new Vector3(0, 0, 0), Quaternion.identity);
        DO.transform.SetParent(gameObject.transform.parent, false);
        DO.transform.localPosition = new Vector3(0, 0, 0);
        DO.transform.SetSiblingIndex(0);
        GameObject tempCard;
        for(int i = 0; i<40; i++){
            tempCard = Instantiate(Card, new Vector3(-500, 0, 0), Quaternion.identity);
            tempCard.transform.SetParent(DO.transform, false);
            tempCard.GetComponent<CardInfo>().Give(Random.Range(1, 9));
            DeckCards.Add(tempCard);
        }
    }

    public void onClick(BaseEventData data){
        PointerEventData Pdata = (PointerEventData)data;
        if((Pdata.pointerId == -1)&&(DO.transform.GetSiblingIndex() == 0)){
            DO.transform.SetSiblingIndex(gameObject.transform.parent.childCount-1);
        }
        
    }
}
