using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public class Cards{
        int id, time, mana, damage;
        string cname, description;
        Vector2 AoE;
        public Cards(int Id, string Cname, int Time, int Mana, int Damage, string Description, Vector2 size){
        id = Id;
        cname = Cname;
        time = Time;
        mana = Mana;
        damage = Damage;
        description = Description;
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
        public Vector2 giveAoE(){
            return AoE;
        }
        public void newDescription(string newD){
            description = newD;
        }
    }

    public List<Cards> cardList = new List<Cards> ();

    void Awake ()
    {
        cardList.Add(new Cards(0, "None", 0, 0, 0, "None", new Vector2(0, 0)));
        cardList.Add(new Cards(1, "Card1", 45, 1, 5, "Deal 5 damage", new Vector2(0, 3)));
        cardList.Add(new Cards(2, "Card2", 30, 2, 2, "Increase the damage of all cards by 1, then deal 2 damage", new Vector2(3, 3)));
        cardList.Add(new Cards(3, "Card3", 90, 2, 3, "Deal 3 damage two times", new Vector2(0, 3)));
        cardList.Add(new Cards(4, "Card4", 15, 1, 0, "Increase the damage of all cards by 2", new Vector2(3, 3)));
        cardList.Add(new Cards(5, "Card5", 30, 1, 3, "Heal 3. Damage altering effects also affects the amount healed", new Vector2(0, 3)));
        cardList.Add(new Cards(6, "Card6", 15, 2, 7, "Heal 7 then increase the damage of all cards by 1", new Vector2(3, 3)));
        cardList.Add(new Cards(7, "Card7", 15, 2, 1, "Deal 1, then draw 1 and gain mana equal to the damage dealt", new Vector2(0, 3)));
        cardList.Add(new Cards(8, "Card8", 0, 1, 10, "Event: The Enemy takes 10 damage", new Vector2(3, 3)));
    }

    public Cards getCard(int id){
        return cardList[id];
    }
}
