using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiscardScript : MonoBehaviour
{
    static public List<GameObject> DiscardedCards = new List<GameObject>();
    public GameObject DO;
    public GameObject DeckOverlay;

    void Awake(){
        Events.Discarded += Discard;
        DO = Instantiate(DeckOverlay, new Vector3(0, 0, 0), Quaternion.identity);
        DO.transform.SetParent(gameObject.transform.parent, false);
        DO.transform.localPosition = new Vector3(0, 0, 0);
        DO.transform.SetSiblingIndex(0);
        DO.name = "DiscardOverlay";
    }

    void Discard(GameObject Card){
        Card.transform.SetParent(DO.transform, false);
        DiscardedCards.Add(Card);
    }

    public void onClick(BaseEventData data){
        PointerEventData Pdata = (PointerEventData)data;
        if((Pdata.pointerId == -1)&&(DO.transform.GetSiblingIndex() <= 1)){
            DO.transform.SetSiblingIndex(gameObject.transform.parent.childCount-1);
        }
        
    }

    void OnDisable(){
        Events.Discarded -= Discard;
    }
}
