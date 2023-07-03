using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawCards : MonoBehaviour
{

    public GameObject Card;
    public GameObject PlayerHand;
    public GameObject DeckOverlay;
    GameObject DO;
    public int DeckCardCount = 0;
    public int CardCount = 0;

    //List<GameObject> cards = new List<GameObject>();
    public List<int> DeckCards = new List<int>();

    void Awake(){
        PlayerHand = GameObject.Find("PlayerHand");
        Events.DrawCardsEvent += DrawC;
        Events.CardDisableEvent += cardDestroyed;
        for(int i = 0; i<40; i++){
            DeckCards.Add(Random.Range(1, 9));
        }
    }

    void Start()
    {
        //cards.Add(Card);
    }

    public void DrawC(int c)
    {

        for (var i = 0; i < c; i++)
        {
            GameObject playerCard = Instantiate(Card, new Vector3(60, 87, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerHand.transform, false);
            playerCard.GetComponent<CardInfo>().Give(DeckCards[DeckCardCount]);
            CardCount++;
            DeckCardCount++;
        }
    }
    public int getCardCount(){
        return CardCount;
    }
    public void cardDestroyed(GameObject Card){
        if(Card != null){
            CardCount--;
        }
    }

    void OnGUI(){
        if (Event.current.Equals(Event.KeyboardEvent("return")) && (DO == null)){
            DO = Instantiate(DeckOverlay, new Vector3(0, 0, 0), Quaternion.identity);
            DO.transform.SetParent(gameObject.transform, false);
        }else if (Event.current.Equals(Event.KeyboardEvent("return")) && (DO != null)){
            
            Destroy(DO);
        }
    }

    void OnDisable(){
        Events.DrawCardsEvent -= DrawC;
        Events.CardDisableEvent -= cardDestroyed;
    }
}
