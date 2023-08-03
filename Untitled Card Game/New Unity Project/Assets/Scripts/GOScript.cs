using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GOScript : MonoBehaviour
{
    float waitTime = 0;
    Color32 c;
    
    public void GameOver(int id){
        waitTime = 3f;
        if(id == 0){
            gameObject.transform.GetComponent<Image>().color = new Color32(0, 255, 0, 0);
        }else{
            gameObject.transform.GetComponent<Image>().color = new Color32(255, 0, 0, 0);
        }
    }
    
    void Update(){
        if((waitTime > 0)&&(waitTime < 1f)){
            c = gameObject.transform.GetComponent<Image>().color;
            c.a += (byte)Time.deltaTime;
            gameObject.transform.GetComponent<Image>().color = c;
            waitTime -= Time.deltaTime;
        }else if(waitTime > 0){
            waitTime -= Time.deltaTime;
        }
    }
}
