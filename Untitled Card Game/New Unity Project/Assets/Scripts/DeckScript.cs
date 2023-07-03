using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public List<int> DeckCards = new List<int>();

    void Start(){
        for(int i = 0; i<40; i++){
            DeckCards.Add(Random.Range(1, 9));
        }
    }
}
