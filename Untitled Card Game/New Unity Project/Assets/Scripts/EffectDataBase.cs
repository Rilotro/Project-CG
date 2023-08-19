using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EffectDataBase : MonoBehaviour
{

    int BonusDMG = 0;
    public GameObject PlayerHealth;

    void PlayerRoundStart(int id){
        if(id == 0){
            Events.Draw(4);
            Events.AddMana(3);
        }
    }

    void PlayerRoundEnd(int id){
        if(id == 0){
            for(int i = 0; i < gameObject.transform.childCount; i++){
                Destroy(gameObject.transform.GetChild(i));
            }
        }
    }

    void Effect1(GameObject Target, Vector2 TAoE, int bDMG){
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Events.DealDMG(bDMG, Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 11); i++){
                Events.DealDMG(bDMG, Target.transform.parent.parent.GetChild((int)pos.x*11+i).GetChild(0).gameObject);
            }
        }else if((TAoE.x != 0)&&(TAoE.y != 0)){
            for(int i = (int)pos.x - ((int)TAoE.x-1)/2; (i <= (int)pos.x + ((int)TAoE.x-1)/2)&&(i < 7); i++){
                if(i < 0){
                    i = 0;
                }
                for(int j = (int)pos.y - ((int)TAoE.y-1)/2; (j <= (int)pos.y + ((int)TAoE.y-1)/2)&&(j < 11); j++){
                    if(j < 0){
                        j = 0;
                    }
                    Events.DealDMG(bDMG, Target.transform.parent.parent.GetChild((int)i*11+j).GetChild(0).gameObject);
                }
            }
        }
    }

    void Effect2(GameObject Target, Vector2 TAoE, int bDMG){
        BonusDMG += 1;
        Events.giveBDMG(1, -1, null);
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Events.DealDMG(bDMG, Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 11); i++){
                Events.DealDMG(bDMG, Target.transform.parent.parent.GetChild((int)pos.x*11+i).GetChild(0).gameObject);
            }
        }else if((TAoE.x != 0)&&(TAoE.y != 0)){
            for(int i = (int)pos.x - ((int)TAoE.x-1)/2; (i <= (int)pos.x + ((int)TAoE.x-1)/2)&&(i < 7); i++){
                if(i < 0){
                    i = 0;
                }
                for(int j = (int)pos.y - ((int)TAoE.y-1)/2; (j <= (int)pos.y + ((int)TAoE.y-1)/2)&&(j < 11); j++){
                    if(j < 0){
                        j = 0;
                    }
                    Events.DealDMG(bDMG, Target.transform.parent.parent.GetChild((int)i*11+j).GetChild(0).gameObject);
                }
            }
        }
    }

    void Effect3(GameObject Target, Vector2 TAoE, int bDMG){
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Events.DealDMG(bDMG, Target);
            Events.DealDMG(bDMG, Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 11); i++){
                Events.DealDMG(bDMG, Target.transform.parent.parent.GetChild((int)pos.x*11+i).GetChild(0).gameObject);
                Events.DealDMG(bDMG, Target.transform.parent.parent.GetChild((int)pos.x*11+i).GetChild(0).gameObject);
            }
        }else if((TAoE.x != 0)&&(TAoE.y != 0)){
            for(int i = (int)pos.x - ((int)TAoE.x-1)/2; (i <= (int)pos.x + ((int)TAoE.x-1)/2)&&(i < 7); i++){
                if(i < 0){
                    i = 0;
                }
                for(int j = (int)pos.y - ((int)TAoE.y-1)/2; (j <= (int)pos.y + ((int)TAoE.y-1)/2)&&(j < 11); j++){
                    if(j < 0){
                        j = 0;
                    }
                    Events.DealDMG(bDMG, Target.transform.parent.parent.GetChild((int)i*11+j).GetChild(0).gameObject);
                    Events.DealDMG(bDMG, Target.transform.parent.parent.GetChild((int)i*11+j).GetChild(0).gameObject);
                }
            }
        }
    }

    void Effect4(GameObject Target, Vector2 TAoE){
        BonusDMG += 2;
        Events.giveBDMG(2, -1, null);
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Target.transform.GetComponent<TileEffects>().GetIgnited();
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 11); i++){
                Target.transform.parent.parent.GetChild((int)pos.x*11+i).GetChild(0).GetComponent<TileEffects>().GetIgnited();
            }
        }else if((TAoE.x != 0)&&(TAoE.y != 0)){
            for(int i = (int)pos.x - ((int)TAoE.x-1)/2; (i <= (int)pos.x + ((int)TAoE.x-1)/2)&&(i < 7); i++){
                if(i < 0){
                    i = 0;
                }
                for(int j = (int)pos.y - ((int)TAoE.y-1)/2; (j <= (int)pos.y + ((int)TAoE.y-1)/2)&&(j < 11); j++){
                    if(j < 0){
                        j = 0;
                    }
                    Target.transform.parent.parent.GetChild((int)i*11+j).GetChild(0).GetComponent<TileEffects>().GetIgnited();
                }
            }
        }
    }

    void Effect5(GameObject Target, Vector2 TAoE, int bDMG){
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Events.DealDMG((bDMG)*(-1), Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 11); i++){
                Events.DealDMG((bDMG)*(-1), Target.transform.parent.parent.GetChild((int)pos.x*11+i).GetChild(0).gameObject);
            }
        }else if((TAoE.x != 0)&&(TAoE.y != 0)){
            for(int i = (int)pos.x - ((int)TAoE.x-1)/2; (i <= (int)pos.x + ((int)TAoE.x-1)/2)&&(i < 7); i++){
                if(i < 0){
                    i = 0;
                }
                for(int j = (int)pos.y - ((int)TAoE.y-1)/2; (j <= (int)pos.y + ((int)TAoE.y-1)/2)&&(j < 11); j++){
                    if(j < 0){
                        j = 0;
                    }
                    Events.DealDMG((bDMG)*(-1), Target.transform.parent.parent.GetChild((int)i*11+j).GetChild(0).gameObject);
                }
            }
        }
    }
    void Effect6(GameObject Target, Vector2 TAoE, int bDMG){
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Events.DealDMG(-bDMG, Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 11); i++){
                Events.DealDMG(-bDMG, Target.transform.parent.parent.GetChild((int)pos.x*11+i).GetChild(0).gameObject);
            }
        }else if((TAoE.x != 0)&&(TAoE.y != 0)){
            for(int i = (int)pos.x - ((int)TAoE.x-1)/2; (i <= (int)pos.x + ((int)TAoE.x-1)/2)&&(i < 7); i++){
                if(i < 0){
                    i = 0;
                }
                for(int j = (int)pos.y - ((int)TAoE.y-1)/2; (j <= (int)pos.y + ((int)TAoE.y-1)/2)&&(j < 11); j++){
                    if(j < 0){
                        j = 0;
                    }
                    Events.DealDMG(-bDMG, Target.transform.parent.parent.GetChild((int)i*11+j).GetChild(0).gameObject);
                }
            }
        }
        BonusDMG += 1;
        Events.giveBDMG(1, -1, null);
    }
    void Effect7(GameObject Target, Vector2 TAoE, int bDMG){
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Events.DealDMG(bDMG, Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 11); i++){
                Events.DealDMG(bDMG, Target.transform.parent.parent.GetChild((int)pos.x*11+i).GetChild(0).gameObject);
            }
        }else if((TAoE.x != 0)&&(TAoE.y != 0)){
            for(int i = (int)pos.x - ((int)TAoE.x-1)/2; (i <= (int)pos.x + ((int)TAoE.x-1)/2)&&(i < 7); i++){
                if(i < 0){
                    i = 0;
                }
                for(int j = (int)pos.y - ((int)TAoE.y-1)/2; (j <= (int)pos.y + ((int)TAoE.y-1)/2)&&(j < 11); j++){
                    if(j < 0){
                        j = 0;
                    }
                    Events.DealDMG(bDMG, Target.transform.parent.parent.GetChild((int)i*11+j).GetChild(0).gameObject);
                }
            }
        }
        Events.Draw(1);
        Events.AddMana(bDMG);
    }
    void Effect8(GameObject Target, Vector2 TAoE, int bDMG){
        Events.addEvent(2, 60, Target, TAoE, new Color32(0, 0, 255, 255));
    }

    public void Cast(GameObject Card, GameObject Target){
        switch (Card.transform.GetComponent<CardInfo>().getId()){
            case 1:
            Effect1(Target, Card.transform.GetComponent<CardInfo>().getAoE(), Card.transform.GetComponent<CardInfo>().getDMG());
            break;
            case 2:
            Effect2(Target, Card.transform.GetComponent<CardInfo>().getAoE(), Card.transform.GetComponent<CardInfo>().getDMG());
            break;
            case 3:
            Effect3(Target, Card.transform.GetComponent<CardInfo>().getAoE(), Card.transform.GetComponent<CardInfo>().getDMG());
            break;
            case 4:
            Effect4(Target, Card.transform.GetComponent<CardInfo>().getAoE());
            break;
            case 5:
            Effect5(Target, Card.transform.GetComponent<CardInfo>().getAoE(), Card.transform.GetComponent<CardInfo>().getDMG());
            break;
            case 6:
            Effect6(Target, Card.transform.GetComponent<CardInfo>().getAoE(), Card.transform.GetComponent<CardInfo>().getDMG());
            break;
            case 7:
            Effect7(Target, Card.transform.GetComponent<CardInfo>().getAoE(), Card.transform.GetComponent<CardInfo>().getDMG());
            break;
            case 8:
            Effect8(Target, Card.transform.GetComponent<CardInfo>().getAoE(), Card.transform.GetComponent<CardInfo>().getDMG());
            break;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public int getBonusDMG(){
        return BonusDMG;
    }

    void Awake()
    {
        Events.CastEvent += Cast;
        Events.RoundStartEvent += PlayerRoundStart;
        gameObject.transform.parent.GetComponent<SceneScript>().givePlayerHand(gameObject);
    }
    void OnDisable(){
        Events.CastEvent -= Cast;
        Events.RoundStartEvent -= PlayerRoundStart;
    }
}
