using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileEffects : MonoBehaviour
{
    public Vector2 pos;
    public static GameObject Player = null;
    Vector2 TAoE = new Vector2(-1, -1);
    public Color32 baseColor;
    bool OnFire = false;

    void Awake()
    {
        Events.DealDMGEvent += EffectDMG;
        Events.RecolorTiles += Recolored;
        Events.giveTilesAoE += getAoe;
        Events.RoundStartEvent += RoundStartRevert;
        baseColor = gameObject.transform.GetComponent<Image>().color;
    }

    void RoundStartRevert(int id){
        if(id == 1){
            baseColor = new Color32(255, 255, 255, 0);
            gameObject.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
    }

    public void EffectDMG(int DMG, GameObject Tile){
        if(gameObject == Tile){
            for(int i = 0; i < gameObject.transform.parent.childCount; i++){
                if(gameObject.transform.parent.GetChild(i).GetComponent<UnitScript>() != null){
                    gameObject.transform.parent.GetChild(i).GetComponent<UnitScript>().TakeDMG(DMG);
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------
    //Fire Effect

    void GetFired(){
        OnFire = true;
        gameObject.transform.parent.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    }

    void FireEffect(){
        if(OnFire){
            for(int i = 0; i < gameObject.transform.parent.childCount; i++){
                if(gameObject.transform.parent.GetChild(i).GetComponent<UnitScript>() != null){
                    gameObject.transform.parent.GetChild(i).GetComponent<UnitScript>().TakeDMG(10);
                }
            }
        }
    }

    void PutOut(){
        OnFire = false;
        gameObject.transform.parent.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------
    //Visuals

    void Recolored(Color32 c){
        if(c.a != 0){
            gameObject.transform.GetComponent<Image>().color = c;
        }else{
            gameObject.transform.GetComponent<Image>().color = baseColor;
        }
    }

    void getAoe(Vector2 size){
        TAoE = size;
    }

    public void onClick(){
        if(Player != null){
            if(gameObject.transform.parent.childCount == 1){
                Vector2 oldPos = Player.transform.GetComponent<UnitScript>().position;
                Player.transform.SetParent(gameObject.transform.parent, false);
                Player.transform.GetComponent<UnitScript>().position = pos;
                Events.AddUnitTime(0, (int)(Mathf.Abs(oldPos.x - pos.x) + Mathf.Abs(oldPos.y - pos.y))*10);
            }
            Player = null;
            Events.Recolor(new Color32(255, 255, 255, 0));
            onEnter();
        }else if(DragnDrop.active != null){
            Events.activateCard(gameObject);
            Events.giveSize(new Vector2(-1, -1));
            Events.Recolor(new Color32(255, 255, 255, 0));
            onEnter();
        }
    }

    public void onEnter(){
        var newC = gameObject.GetComponent<Image>().color;
        if((Player == null)&&(DragnDrop.active == null)){
            newC.a += 0.5f;
        }else if(Player != null){
            newC.a += 0.25f;
        }else if(DragnDrop.active != null){
            newC.a += 0.25f;
            newC.r += 255;
            newC.g -= 255;
        }
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            gameObject.GetComponent<Image>().color = newC;
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 8); i++){
                gameObject.transform.parent.parent.GetChild((int)pos.x*8+i).GetChild(0).GetComponent<Image>().color = newC;
            }
        }else if((TAoE.x != 0)&&(TAoE.y != 0)){
            for(int i = (int)pos.x - ((int)TAoE.x-1)/2; (i <= (int)pos.x + ((int)TAoE.x-1)/2)&&(i < 4); i++){
                if(i < 0){
                    i = 0;
                }
                for(int j = (int)pos.y - ((int)TAoE.y-1)/2; (j <= (int)pos.y + ((int)TAoE.y-1)/2)&&(j < 8); j++){
                    if(j < 0){
                        j = 0;
                    }
                    gameObject.transform.parent.parent.GetChild((int)i*8+j).GetChild(0).GetComponent<Image>().color = newC;
                }
            }
        }
    }

    public void OnExit(){
        var newC = gameObject.GetComponent<Image>().color;
        if((Player == null)&&(DragnDrop.active == null)){
            newC.a -= 0.5f;
        }else if(Player != null){
            newC.a -= 0.25f;
        }else if(DragnDrop.active != null){
            newC.a -= 0.25f;
            newC.r -= 255;
            newC.g += 255;
        }
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            gameObject.GetComponent<Image>().color = newC;
        }else{
            Events.Recolor(newC);
        }
    }

    void OnDisable(){
        Events.DealDMGEvent -= EffectDMG;
        Events.RecolorTiles -= Recolored;
        Events.giveTilesAoE -= getAoe;
        Events.RoundStartEvent -= RoundStartRevert;
    }
}
