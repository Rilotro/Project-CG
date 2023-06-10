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
    bool grounded = false;
    public Vector2 AoE;

    public void Give(int newId){
        Events.RoundEndEvent += DestroyMe;

        id = newId;
        description = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveDescription();
        time = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveTime();
        mana = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveMana();
        cname = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveName();
        AoE = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveAoE();
        baseDamage = gameObject.transform.parent.GetComponent<CardDataBase>().cardList[newId].giveDMG();
        assignInfo();
        if(id == 1){
            gameObject.transform.parent.GetComponent<EffectDataBase>().Ground(id, gameObject);
        }
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
        gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = mana.ToString();
        gameObject.transform.GetChild(1).GetComponent<Text>().text = description;
        gameObject.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = time.ToString();
    }

    public void Ground(BaseEventData data)
    {
        PointerEventData Pdata = (PointerEventData)data;
        if(Pdata.pointerId == -2){
            if(grounded == false){
                grounded = true;
                gameObject.transform.GetComponent<Image>().color = new Color32(50, 25, 0, 255);
            }else{
                grounded = false;
                gameObject.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }
    }

    // Update is called once per frame
    void DestroyMe(int id){
        if((id == 0)&&(grounded == false)){
            Destroy(gameObject);
        }
    }

    void OnDisable(){
        Events.RoundEndEvent -= DestroyMe;
    }
}
