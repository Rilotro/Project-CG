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
            Events.DealDMG(bDMG+BonusDMG, Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 8); i++){
                Events.DealDMG(bDMG+BonusDMG, Target.transform.parent.parent.GetChild((int)pos.x*8+i).GetChild(0).gameObject);
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
                    Events.DealDMG(bDMG+BonusDMG, Target.transform.parent.parent.GetChild((int)i*8+j).GetChild(0).gameObject);
                }
            }
        }
    }

    void Effect2(GameObject Target, Vector2 TAoE, int bDMG){
        BonusDMG += 1;
        updateDMGDesc();
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Events.DealDMG(bDMG+BonusDMG, Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 8); i++){
                Events.DealDMG(bDMG+BonusDMG, Target.transform.parent.parent.GetChild((int)pos.x*8+i).GetChild(0).gameObject);
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
                    Events.DealDMG(bDMG+BonusDMG, Target.transform.parent.parent.GetChild((int)i*8+j).GetChild(0).gameObject);
                }
            }
        }
    }

    void Effect3(GameObject Target, Vector2 TAoE, int bDMG){
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Events.DealDMG(bDMG+BonusDMG, Target);
            Events.DealDMG(bDMG+BonusDMG, Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 8); i++){
                Events.DealDMG(bDMG+BonusDMG, Target.transform.parent.parent.GetChild((int)pos.x*8+i).GetChild(0).gameObject);
                Events.DealDMG(bDMG+BonusDMG, Target.transform.parent.parent.GetChild((int)pos.x*8+i).GetChild(0).gameObject);
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
                    Events.DealDMG(bDMG+BonusDMG, Target.transform.parent.parent.GetChild((int)i*8+j).GetChild(0).gameObject);
                    Events.DealDMG(bDMG+BonusDMG, Target.transform.parent.parent.GetChild((int)i*8+j).GetChild(0).gameObject);
                }
            }
        }
    }

    void Effect4(){
        BonusDMG += 2;
        updateDMGDesc();
    }
    void Effect5(GameObject Target, Vector2 TAoE, int bDMG){
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Events.DealDMG((bDMG+BonusDMG)*(-1), Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 8); i++){
                Events.DealDMG((bDMG+BonusDMG)*(-1), Target.transform.parent.parent.GetChild((int)pos.x*8+i).GetChild(0).gameObject);
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
                    Events.DealDMG((bDMG+BonusDMG)*(-1), Target.transform.parent.parent.GetChild((int)i*8+j).GetChild(0).gameObject);
                }
            }
        }
    }
    void Effect6(GameObject Target, Vector2 TAoE, int bDMG){
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Events.DealDMG(-bDMG, Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 8); i++){
                Events.DealDMG(-bDMG, Target.transform.parent.parent.GetChild((int)pos.x*8+i).GetChild(0).gameObject);
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
                    Events.DealDMG(-bDMG, Target.transform.parent.parent.GetChild((int)i*8+j).GetChild(0).gameObject);
                }
            }
        }
        BonusDMG += 1;
        updateDMGDesc();
    }
    void Effect7(GameObject Target, Vector2 TAoE, int bDMG){
        Vector2 pos = Target.GetComponent<TileEffects>().pos;
        if((TAoE.x < 0)&&(TAoE.y < 0)){
            Events.DealDMG(bDMG+BonusDMG, Target);
        }else if(TAoE.x == 0){
            for(int i = (int)pos.y; (i < (int)pos.y+(int)TAoE.y)&&(i < 8); i++){
                Events.DealDMG(bDMG+BonusDMG, Target.transform.parent.parent.GetChild((int)pos.x*8+i).GetChild(0).gameObject);
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
                    Events.DealDMG(bDMG+BonusDMG, Target.transform.parent.parent.GetChild((int)i*8+j).GetChild(0).gameObject);
                }
            }
        }
        Events.Draw(1);
        Events.AddMana(bDMG+BonusDMG);
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
            Effect4();
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

    public void Ground(int id, GameObject Card){
        Events.addEvent(3, 60, Card, new Vector2(-1, -1), new Color32(255, 255, 255, 0));
    }

    public int getBonusDMG(){
        return BonusDMG;
    }

    void updateDMGDesc(){
        gameObject.GetComponent<CardDataBase>().getCard(1).newDescription(string.Format("Deal *{0}* damage", 5+BonusDMG));
        gameObject.GetComponent<CardDataBase>().getCard(2).newDescription(string.Format("Increase the damage of all cards by 1, then deal *{0}* damage", 2+BonusDMG));
        gameObject.GetComponent<CardDataBase>().getCard(3).newDescription(string.Format("Deal *{0}* damage two times", 3+BonusDMG));
        gameObject.GetComponent<CardDataBase>().getCard(5).newDescription(string.Format("Heal *{0}*. Damage altering effects also affects the amount healed by this card", 3+BonusDMG));
        gameObject.GetComponent<CardDataBase>().getCard(7).newDescription(string.Format("Deal *{0}*, then draw cards and gain mana equal to that ammount", 1+BonusDMG));

        for(int i = 0; i < gameObject.transform.childCount; i++){
            switch (gameObject.transform.GetChild(i).GetComponent<CardInfo>().getId()){
            case 1:
            gameObject.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = string.Format("Deal *{0}* damage", 5+BonusDMG);
            break;
            case 2:
            gameObject.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = string.Format("Increase the damage of all cards by 1, then deal *{0}* damage", 2+BonusDMG);
            break;
            case 3:
            gameObject.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = string.Format("Deal *{0}* damage two times", 3+BonusDMG);
            break;
            case 5:
            gameObject.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = string.Format("Heal *{0}*. Damage altering effects also affects the amount healed by this card", 3+BonusDMG);
            break;
            case 7:
            gameObject.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = string.Format("Deal *{0}*, then draw cards and gain mana equal to that ammount", 1+BonusDMG);
            break;
        }
        }
    }
    void Awake()
    {
        Events.CastEvent += Cast;
        Events.RoundStartEvent += PlayerRoundStart;
    }
    void OnDisable(){
        Events.CastEvent -= Cast;
        Events.RoundStartEvent -= PlayerRoundStart;
    }
}
