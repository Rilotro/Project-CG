using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCounterScript : MonoBehaviour
{
    int counter = 0;
    public void onClickAdd(){
        if(counter < 3){
            counter++;
            gameObject.transform.GetChild(2).GetComponent<Text>().text = counter.ToString();
        }
    }

    public void onClickRemove(){
        if(counter > 0){
            counter--;
            gameObject.transform.GetChild(2).GetComponent<Text>().text = counter.ToString();
        }
    }
}
