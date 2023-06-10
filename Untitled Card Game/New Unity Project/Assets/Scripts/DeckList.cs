using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckList : MonoBehaviour
{

    public GameObject Card;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= 8; i++){
            GameObject playerCard = Instantiate(Card, new Vector3(60, 87, 0), Quaternion.identity);
            playerCard.transform.SetParent(gameObject.transform, false);
            playerCard.GetComponent<CardInfo>().Give(i);
        }
    }

    // Update is called once per frame
}
