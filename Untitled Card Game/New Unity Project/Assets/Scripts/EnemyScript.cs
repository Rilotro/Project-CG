using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyScript : UnitScript
{

    GameObject MainCanvas;
    int intent = 0;
    Vector2 PlayerPos;
    Vector2 TAoE;
    GameObject PlayerTile;
    float x, y;

    void Start()
    {
        Events.RoundStartEvent += RoundStart;
        Events.AssignTile(position, gameObject);
        ChooseIntent();
    }

    public void RoundStart(int id){
        if(id == 1){
            if(intent < 0){
                Events.AddUnitTime(1, 15 * (int)(Mathf.Abs(position.x - x) + Mathf.Abs(position.y - y)));
                position = new Vector2(x, y);
                Events.AssignTile(position, gameObject);
                ChooseIntent();
            }else if(intent < 70){
                if((TAoE.x < 0)&&(TAoE.y < 0)){
                    Events.DealDMG(10, PlayerTile.transform.GetChild(0).gameObject);
                }else if(TAoE.x == 0){
                    for(int i = (int)PlayerPos.y; (i < (int)PlayerPos.y+(int)TAoE.y)&&(i < 8); i++){
                        Events.DealDMG(10, PlayerTile.transform.parent.GetChild((int)PlayerPos.x*8+i).GetChild(0).gameObject);
                    }
                }else if((TAoE.x != 0)&&(TAoE.y != 0)){
                    for(int i = (int)PlayerPos.x - ((int)TAoE.x-1)/2; (i <= (int)PlayerPos.x + ((int)TAoE.x-1)/2)&&(i < 4); i++){
                        if(i < 0){
                            i = 0;
                        }
                        for(int j = (int)PlayerPos.y - ((int)TAoE.y-1)/2; (j <= (int)PlayerPos.y + ((int)TAoE.y-1)/2)&&(j < 8); j++){
                            if(j < 0){
                                j = 0;
                            }
                            Events.DealDMG(10, PlayerTile.transform.parent.GetChild((int)i*8+j).GetChild(0).gameObject);
                        }
                    }
                }
                ChooseIntent();
                Events.AddUnitTime(1, 60);
            }
            else{
                if((TAoE.x < 0)&&(TAoE.y < 0)){
                    Events.DealDMG(15, PlayerTile.transform.GetChild(0).gameObject);
                }else if(TAoE.x == 0){
                    for(int i = (int)PlayerPos.y; (i < (int)PlayerPos.y+(int)TAoE.y)&&(i < 8); i++){
                        Events.DealDMG(15, PlayerTile.transform.parent.GetChild((int)PlayerPos.x*8+i).GetChild(0).gameObject);
                    }
                }else if((TAoE.x != 0)&&(TAoE.y != 0)){
                    for(int i = (int)PlayerPos.x - ((int)TAoE.x-1)/2; (i <= (int)PlayerPos.x + ((int)TAoE.x-1)/2)&&(i < 4); i++){
                        if(i < 0){
                            i = 0;
                        }
                        for(int j = (int)PlayerPos.y - ((int)TAoE.y-1)/2; (j <= (int)PlayerPos.y + ((int)TAoE.y-1)/2)&&(j < 8); j++){
                            if(j < 0){
                                j = 0;
                            }
                            Events.DealDMG(15, PlayerTile.transform.parent.GetChild((int)i*8+j).GetChild(0).gameObject);
                        }
                    }
                }
                ChooseIntent();
                Events.AddUnitTime(1, 90);
            }
            Events.RoundEnd(1);
        }
    }

    public void ChooseIntent(){
        intent = Random.Range(0, 100);
        if(intent < 70){
            gameObject.transform.GetChild(1).GetComponent<Text>().text = string.Format("{0} - {1}", 10, 60);
            TAoE = new Vector2(3, 3);
        }else{
            gameObject.transform.GetChild(1).GetComponent<Text>().text = string.Format("{0} - {1}", 15, 90);
            TAoE = new Vector2(5, 5);
        }
        for(int i = 0; i < gameObject.transform.parent.parent.childCount; i++){
            if(gameObject.transform.parent.parent.GetChild(i).childCount > 1){
                if(gameObject.transform.parent.parent.GetChild(i).GetChild(1).name == "Player"){
                    PlayerTile = gameObject.transform.parent.parent.GetChild(i).gameObject;
                    PlayerPos = gameObject.transform.parent.parent.GetChild(i).GetChild(0).GetComponent<TileEffects>().pos;
                }
            }
        }
        Color32 newC = new Color32(112, 0, 0, 50);
        if(Mathf.Abs(position.x - PlayerPos.x) + Mathf.Abs(position.y - PlayerPos.y) < 5){
            if((TAoE.x < 0)&&(TAoE.y < 0)){
                PlayerTile.transform.GetChild(0).GetComponent<Image>().color = newC;
                PlayerTile.transform.GetChild(0).GetComponent<TileEffects>().baseColor = newC;
            }else if(TAoE.x == 0){
                for(int i = (int)PlayerPos.y; (i < (int)PlayerPos.y+(int)TAoE.y)&&(i < 8); i++){
                    PlayerTile.transform.parent.GetChild((int)PlayerPos.x*8+i).GetChild(0).GetComponent<Image>().color = newC;
                    PlayerTile.transform.parent.GetChild((int)PlayerPos.x*8+i).GetChild(0).GetComponent<TileEffects>().baseColor = newC;
                }
            }else if((TAoE.x != 0)&&(TAoE.y != 0)){
                for(int i = (int)PlayerPos.x - ((int)TAoE.x-1)/2; (i <= (int)PlayerPos.x + ((int)TAoE.x-1)/2)&&(i < 4); i++){
                    if(i < 0){
                        i = 0;
                    }
                    for(int j = (int)PlayerPos.y - ((int)TAoE.y-1)/2; (j <= (int)PlayerPos.y + ((int)TAoE.y-1)/2)&&(j < 8); j++){
                        if(j < 0){
                            j = 0;
                        }
                        PlayerTile.transform.parent.GetChild((int)i*8+j).GetChild(0).GetComponent<Image>().color = newC;
                        PlayerTile.transform.parent.GetChild((int)i*8+j).GetChild(0).GetComponent<TileEffects>().baseColor = newC;
                    }
                }
            }
        }else{
            intent = -1;
            x = position.x;
            y = position.y;
            int count = 0;
            if(Mathf.Abs(position.x - PlayerPos.x) < Mathf.Abs(position.y - PlayerPos.y)){
                while((Mathf.Abs(x - PlayerPos.x) > 0)&&(count < 5)){
                    if(PlayerPos.x > position.x){
                        x++;
                        count++;
                    }else{
                        x--;
                        count++;
                    }
                }
                while((Mathf.Abs(y - PlayerPos.y) > 1)&&(count < 5)){
                    if(PlayerPos.y > position.y){
                        y++;
                        count++;
                    }else{
                        y--;
                        count++;
                    }
                }
            }else{
                while((Mathf.Abs(y - PlayerPos.y) > 0)&&(count < 5)){
                    if(PlayerPos.y > position.y){
                        y++;
                        count++;
                    }else{
                        y--;
                        count++;
                    }
                }
                while((Mathf.Abs(x - PlayerPos.x) > 1)&&(count < 5)){
                    if(PlayerPos.x > position.x){
                        x++;
                        count++;
                    }else{
                        x--;
                        count++;
                    }
                }
            }
            PlayerTile.transform.parent.GetChild((int)x*8+(int)y).GetChild(0).GetComponent<Image>().color = newC;
            PlayerTile.transform.parent.GetChild((int)x*8+(int)y).GetChild(0).GetComponent<TileEffects>().baseColor = newC;
        }
    }

    void OnDisable(){
        Events.RoundStartEvent -= RoundStart;
    }
}
