﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{

    public GameObject Card;

    public class Cards{
        int id, time, mana, damage;
        string cname, description, tag;
        Vector2 AoE;
        public Cards(int Id, string Cname, int Time, int Mana, int Damage, string Description, string Tag, Vector2 size){
        id = Id;
        cname = Cname;
        time = Time;
        mana = Mana;
        damage = Damage;
        description = Description;
        tag = Tag;
        AoE = size;
        }
        public int giveId(){
            return id;
        }
        public int giveTime(){
            return time;
        }
        public int giveMana(){
            return mana;
        }
        public string giveName(){
            return cname;
        }
        public int giveDMG(){
            return damage;
        }
        public string giveDescription(){
            return description;
        }
        public string giveTag(){
            return tag;
        }
        public Vector2 giveAoE(){
            return AoE;
        }
        public void newDescription(string newD){
            description = newD;
        }
        public void newDMG(int newDamage){
            damage = newDamage;
        }
    }

    public List<Cards> cardList = new List<Cards> ();

    void Awake ()
    {
        Events.updateDescEvent += updateDMGDesc;
        Events.ReloadEvent += Reload;
        cardList.Add(new Cards(0, "None", 0, 0, 0, "None", "None", new Vector2(0, 0)));
        cardList.Add(new Cards(1, "Card1", 45, 1, 5, "Grounded(60):Increase this cards damage by 5.\nDeal 5 damage", "damage", new Vector2(0, 3)));//GROUND(X):the effect takes place once every X event seconds, while the card is in hand.
        cardList.Add(new Cards(2, "Card2", 30, 2, 2, "Increase the damage of all cards by 1, then deal 2 damage", "increase, damage", new Vector2(3, 3)));
        cardList.Add(new Cards(3, "Card3", 90, 2, 3, "Deal 3 damage two times", "damage", new Vector2(0, 3)));
        cardList.Add(new Cards(4, "Card4", 15, 1, 0, "Ignite\nIncrease the damage of all cards by 2", "fire, increase", new Vector2(3, 3)));//Ignite:the tiles in the AoE of the card catch fire, dealing 15 damage every 30 event seconds.
        cardList.Add(new Cards(5, "Card5", 30, 1, 3, "Heal 3. Damage altering effects also affects the amount healed", "heal", new Vector2(0, 3)));
        cardList.Add(new Cards(6, "Card6", 15, 2, 7, "Heal 7 then increase the damage of all cards by 1", "heal, increase", new Vector2(3, 3)));
        cardList.Add(new Cards(7, "Card7", 15, 2, 1, "Deal 1, then draw 1 and gain mana equal to the damage dealt", "damage", new Vector2(0, 3)));
        cardList.Add(new Cards(8, "Card8", 0, 1, 10, "Event(60):Ignite. The Enemy takes 10 damage", "fire, damage", new Vector2(3, 3)));
    }

    public void updateDMGDesc(int BDMG, int id, GameObject Target){
        if(Target == null){
            if(id<0){
                cardList[1].newDescription(string.Format("Grounded(60):Increase this cards damage by 5\nDeal *{0}* damage", cardList[1].giveDMG()+BDMG));
                cardList[1].newDMG(cardList[1].giveDMG()+BDMG);
                cardList[2].newDescription(string.Format("Increase the damage of all cards by 1, then deal *{0}* damage", cardList[2].giveDMG()+BDMG));
                cardList[2].newDMG(cardList[2].giveDMG()+BDMG);
                cardList[3].newDescription(string.Format("Deal *{0}* damage two times", cardList[3].giveDMG()+BDMG));
                cardList[3].newDMG(cardList[3].giveDMG()+BDMG);
                cardList[5].newDescription(string.Format("Heal *{0}*. Damage altering effects also affects the amount healed by this card", cardList[5].giveDMG()+BDMG));
                cardList[5].newDMG(cardList[5].giveDMG()+BDMG);
                cardList[7].newDescription(string.Format("Deal *{0}*, then draw cards and gain mana equal to that ammount", cardList[7].giveDMG()+BDMG));
                cardList[7].newDMG(cardList[7].giveDMG()+BDMG);
            }else if(id>0){
                switch(id){
                    case 1:
                    cardList[1].newDescription(string.Format("GROUND(60):Increase this cards damage by 5\nDeal *{0}* damage", cardList[1].giveDMG()+BDMG));
                    cardList[1].newDMG(cardList[1].giveDMG()+BDMG);
                    break;
                    case 2:
                    cardList[2].newDescription(string.Format("Increase the damage of all cards by 1, then deal *{0}* damage", cardList[2].giveDMG()+BDMG));
                    cardList[2].newDMG(cardList[2].giveDMG()+BDMG);
                    break;
                    case 3:
                    cardList[3].newDescription(string.Format("Deal *{0}* damage two times", cardList[3].giveDMG()+BDMG));
                    cardList[3].newDMG(cardList[3].giveDMG()+BDMG);
                    break;
                    case 5:
                    cardList[5].newDescription(string.Format("Heal *{0}*. Damage altering effects also affects the amount healed by this card", cardList[5].giveDMG()+BDMG));
                    cardList[5].newDMG(cardList[5].giveDMG()+BDMG);
                    break;
                    case 7:
                    cardList[7].newDescription(string.Format("Deal *{0}*, then draw cards and gain mana equal to that ammount", cardList[7].giveDMG()+BDMG));
                    cardList[7].newDMG(cardList[7].giveDMG()+BDMG);
                    break;
                }
            }
        }
    }

    public Cards getCard(int id){
        return cardList[id];
    }

    void Reload(){
        cardList[1].newDescription("Grounded(60):Increase this cards damage by 5\nDeal 5 damage");
        cardList[1].newDMG(5);
        cardList[2].newDescription("Increase the damage of all cards by 1, then deal 2 damage");
        cardList[2].newDMG(2);
        cardList[3].newDescription("Deal 3 damage two times");
        cardList[3].newDMG(3);
        cardList[5].newDescription("Heal 3. Damage altering effects also affects the amount healed by this card");
        cardList[5].newDMG(3);
        cardList[7].newDescription("Deal 1, then draw cards and gain mana equal to that ammount");
        cardList[7].newDMG(1);
    }

    void OnDisable(){
        Events.updateDescEvent -= updateDMGDesc;
        Events.ReloadEvent -= Reload;
    }
}
