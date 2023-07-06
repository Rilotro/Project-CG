using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawCards : MonoBehaviour
{
    void Awake(){
        Events.DrawCardsEvent += DrawC;
    }

    public void DrawC(int c)
    {
        int index;
        for(int i = 0; i < c; i++){
            index = Random.Range(0, DeckScript.DeckCards.Count);
            DeckScript.DeckCards[index].transform.SetParent(gameObject.transform, false);
            DeckScript.DeckCards.Remove(DeckScript.DeckCards[index]);
        }
    }

    void OnDisable(){
        Events.DrawCardsEvent -= DrawC;
    }
}
