using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRelocation : MonoBehaviour
{

    void Awake(){
        Events.Relocate += BeginRelocation;
    }

    public void BeginRelocation(){
        Events.Recolor(new Color32(0, 0, 255, 50));
        for(int i = 0; i < gameObject.transform.childCount; i++){
            if(gameObject.transform.GetChild(i).childCount > 1){
                if(gameObject.transform.GetChild(i).GetChild(1).name == "Player"){
                    TileEffects.Player = gameObject.transform.GetChild(i).GetChild(1).gameObject;
                }
            }
        }
        if(DragnDrop.active != null){
            DragnDrop.active = null;
            Events.giveSize(new Vector2(-1, -1));
        }
    }

    void OnDisable(){
        Events.Relocate -= BeginRelocation;
    }
}
