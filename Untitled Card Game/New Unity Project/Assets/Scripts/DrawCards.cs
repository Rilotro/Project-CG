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
    public int Id;
    public int CardCount = 0;

    List<GameObject> cards = new List<GameObject>();

    void Awake(){
        PlayerHand = GameObject.Find("PlayerHand");
        Events.DrawCardsEvent += DrawC;
        Events.CastEvent += cardDestroyed;
        Events.RoundEndEvent += DestroyAllCards;
    }

    void Start()
    {
        cards.Add(Card);
    }

    public void DrawC(int c)
    {

        for (var i = 0; i < c; i++)
        {
            Id = Random.Range(1, 9);
            GameObject playerCard = Instantiate(Card, new Vector3(60, 87, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerHand.transform, false);
            playerCard.GetComponent<CardInfo>().Give(Id);
            CardCount++;
        }
    }
    public int getCardCount(){
        return CardCount;
    }
    public void cardDestroyed(GameObject Card, GameObject Target){
        if(Target != null){
            CardCount--;
        }
    }

    void DestroyAllCards(int id){
        if(id == 0){
            CardCount = 0;
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
        Events.CastEvent -= cardDestroyed;
        Events.RoundEndEvent -= DestroyAllCards;
    }
}
