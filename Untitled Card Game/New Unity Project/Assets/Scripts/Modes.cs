using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Modes : MonoBehaviour
{
    void Update(){
        if(Input.GetMouseButtonDown(1)){
            print("clicked");
            DeckScript.DO.transform.SetSiblingIndex(0);
            Events.Recolor(new Color32(255, 255, 255, 0));
            DragnDrop.active = null;
            TileEffects.Player = null;
        }
    }
}
