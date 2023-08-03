using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Modes : MonoBehaviour
{
    void Update(){
        if(Input.GetMouseButtonDown(1)){
            print("clicked");
            if(gameObject.transform.GetChild(gameObject.transform.childCount-1).name.Contains("Overlay") == true){
                if(gameObject.transform.GetChild(gameObject.transform.childCount-1).name.Contains("AddCardsOverlay") == true){
                    Destroy(gameObject.transform.GetChild(gameObject.transform.childCount-1).gameObject);
                }else{
                    gameObject.transform.GetChild(gameObject.transform.childCount-1).SetSiblingIndex(0);
                }
            }
            Events.Recolor(new Color32(255, 255, 255, 0));
            DragnDrop.active = null;
            TileEffects.Player = null;
        }
    }
}
