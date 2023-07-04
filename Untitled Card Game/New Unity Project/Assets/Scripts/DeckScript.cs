using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckScript : MonoBehaviour
{
    static public List<int> DeckCards = new List<int>();
    static public GameObject DO;
    public GameObject DeckOverlay;

    void Awake(){
        for(int i = 0; i<40; i++){
            DeckCards.Add(Random.Range(1, 9));
        }
    }

    public void onClick(BaseEventData data){
        PointerEventData Pdata = (PointerEventData)data;
        if((Pdata.pointerId == -1)&&(DO == null)){
            DO = Instantiate(DeckOverlay, new Vector3(0, 0, 0), Quaternion.identity);
            DO.transform.SetParent(gameObject.transform, false);
        }
        
    }
}
