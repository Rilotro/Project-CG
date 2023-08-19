using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardInfo : MonoBehaviour
{

    public int id;
    public string description;
    public int time;
    public int mana;
    public int baseDamage;
    public string cname;
    public string Ctag;
    bool grounded = false;
    public Vector2 AoE;

    public void Give(int newId){
        Events.RoundEndEvent += DestroyMe;
        Events.updateDescEvent += updateDesc;
        Events.ReloadEvent += End;

        id = newId;
        description = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveDescription();
        time = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveTime();
        mana = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveMana();
        cname = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveName();
        AoE = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveAoE();
        baseDamage = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveDMG();
        Ctag = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveTag();
        assignInfo();
    }

    public int getId(){
        return id;
    }

    public int getCost(){
        return mana;
    }

    public int getTime(){
        return time;
    }

    public Vector2 getAoE(){
        return AoE;
    }

    public int getDMG(){
        return baseDamage;
    }

    public GameObject getCard(){
        return gameObject;
    }

    // Start is called before the first frame update
    void assignInfo()
    {
        gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = mana.ToString();
        gameObject.transform.GetChild(2).GetComponent<Text>().text = description;
        gameObject.transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = time.ToString();
        gameObject.tag = Ctag;
    }

    public void updateDesc(int BDMG, int Bid, GameObject Target){
        if((Bid == id)||(Bid<0)||(Target == gameObject)){
            switch (id){
                    case 1:
                    baseDamage += BDMG;
                    gameObject.transform.GetChild(2).GetComponent<Text>().text = string.Format("GROUND(60):Increase this cards damage by 5\nDeal *{0}* damage", baseDamage);
                    break;
                    case 2:
                    baseDamage += BDMG;
                    gameObject.transform.GetChild(2).GetComponent<Text>().text = string.Format("Increase the damage of all cards by 1, then deal *{0}* damage", baseDamage);
                    break;
                    case 3:
                    baseDamage += BDMG;
                    gameObject.transform.GetChild(2).GetComponent<Text>().text = string.Format("Deal *{0}* damage two times", baseDamage);
                    break;
                    case 5:
                    baseDamage += BDMG;
                    gameObject.transform.GetChild(2).GetComponent<Text>().text = string.Format("Heal *{0}*. Damage altering effects also affects the amount healed by this card", baseDamage);
                    break;
                    case 7:
                    baseDamage += BDMG;
                    gameObject.transform.GetChild(2).GetComponent<Text>().text = string.Format("Deal *{0}*, then draw cards and gain mana equal to that ammount", baseDamage);
                    break;
                }
        }
    }

    public void Ground(BaseEventData data)
    {
        PointerEventData Pdata = (PointerEventData)data;
        if(Pdata.pointerId == -2){
            if(grounded == false){
                grounded = true;
                gameObject.transform.GetComponent<Image>().color = new Color32(50, 25, 0, 255);
                if(id == 1){
                    Events.addEvent(3, 60, gameObject, new Vector2(-1, -1), new Color32(255, 255, 255, 0));
                }
            }else{
                grounded = false;
                gameObject.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                if(id == 1){
                    Events.Ungrounded(gameObject);
                }
            }
        }
    }

    void DestroyMe(int id){
        if((id == 0)&&(grounded == false)&&(gameObject.transform.parent.name == "PlayerHand")){
            Events.Discard(gameObject);
        }
    }

    void End(){
        Destroy(gameObject);
    }

    void OnDisable(){
        Events.Ungrounded(gameObject);
        Events.onCardDisable(gameObject);
        Events.RoundEndEvent -= DestroyMe;
        Events.updateDescEvent -= updateDesc;
        Events.ReloadEvent -= End;
    }
}
