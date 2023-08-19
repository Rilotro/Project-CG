using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlayingBT : MonoBehaviour
{
    //bool PlayerRound = false;
    float waitTime = 0;
    int i, mana = 0;
    GameObject Background;
    GameObject Enemy;
    GameObject Player;
    public bool heal = false;
    public bool fire = false;
    public bool DMG = false;
    public bool increase = false;
    bool unplayed = true;
    public float EstimDMGpC = 0;
    public float EstimDMGpT = 0;
    public float EstimTpC = 0;
    public float EstimDMGpM = 0;
    static List<int> test = new List<int>();

    class CardCount{
        public int id1, id2, id3, id4, id7, id8;
        public CardCount(){
            id1 = 0;
            id2 = 0;
            id3 = 0;
            id4 = 0;
            id7 = 0;
            id8 = 0;
        }
    }

    CardCount deck = new CardCount();

    void Awake(){
        Events.RoundStartEvent += StartPlayerTurn;
        Events.CastEvent += NextCard;
        Events.GetMana += GetMana;
        for(i = 0; i < 3; i++){
            if(gameObject.transform.parent.GetChild(i).name == "Background"){
                Background = gameObject.transform.parent.GetChild(i).gameObject;
            }
        }
        test.Add(Random.Range(1, 9));
        print(test.Count);
    }

    void Start(){
        for(i = 0; i < Background.transform.childCount; i++){
            if((Background.transform.GetChild(i).childCount > 1)&&(Background.transform.GetChild(i).GetChild(1).name.Contains("Enemy") == true)){
                Enemy = Background.transform.GetChild(i).GetChild(1).gameObject;
            }else if((Background.transform.GetChild(i).childCount > 1)&&(Background.transform.GetChild(i).GetChild(1).name.Contains("Player") == true)){
                Player = Background.transform.GetChild(i).GetChild(1).gameObject;
            }
        }
        if((Enemy == null)||(Player == null)){
                print("oops");
        }

        for(i = 0; i < DeckScript.DeckCards.Count; i++){
            switch(DeckScript.DeckCards[i].GetComponent<CardInfo>().id){
                case 1:
                deck.id1++;
                break;
                case 2:
                deck.id2++;
                break;
                case 3:
                deck.id3++;
                break;
                case 7:
                deck.id7++;
                break;
                case 8:
                deck.id8++;
                break;
            }
        }
        for(i = 0; i < gameObject.transform.childCount; i++){
            switch(gameObject.transform.GetChild(i).GetComponent<CardInfo>().id){
                case 1:
                deck.id1++;
                break;
                case 2:
                deck.id2++;
                break;
                case 3:
                deck.id3++;
                break;
                case 7:
                deck.id7++;
                break;
                case 8:
                deck.id8++;
                break;
            }
        }

        
    }

    void Update(){
        if(waitTime > 0){
            waitTime -= Time.deltaTime;
        }else if(unplayed == true){
            unplayed = false;
            PlayCard();
        }
    }

    void GetMana(int newMana){
        mana = newMana;
    }

    void PlayCard(){
        List<int> playable = new List<int>();
        deck.id4 = 0;
        for(i = 0; i < gameObject.transform.childCount; i++){
            if(gameObject.transform.GetChild(i).GetComponent<CardInfo>().mana <= mana){
                playable.Add(i);
                if(gameObject.transform.GetChild(i).GetComponent<CardInfo>().id == 4){
                    deck.id4++;
                }
            }
        }

        if(playable.Count > 0){
            checkTags(playable);
            int play = -1;
            if(deck.id7 > 0){
                for(i = 0; i < playable.Count; i++){
                    if((gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().id == 7)&&((play < 0)||(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage > gameObject.transform.GetChild(playable[play]).GetComponent<CardInfo>().baseDamage))){
                        if(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage > 1){
                            play = i;
                        }
                    }
                }
            }
            if(play >= 0){
                print(playable[play]);
                gameObject.transform.GetChild(playable[play]).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            }else if((heal == true)&&((Player.transform.GetChild(0).GetComponent<Health>().currentHP < Enemy.transform.GetChild(0).GetComponent<Health>().currentHP)||((fire == false)&&(DMG == false)&&(increase == false)))){
                print("heal!");
                for(i = 0; i < playable.Count; i++){
                    if(gameObject.transform.GetChild(playable[i]).tag.Contains("heal") == true){
                        if((play < 0)||(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage > gameObject.transform.GetChild(playable[play]).GetComponent<CardInfo>().baseDamage)){
                            play = i;
                        }
                    }
                }
                print(playable[play]);
                gameObject.transform.GetChild(playable[play]).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            }else{
                print("not heal");
                if((deck.id4 > 0)){
                    DMGEstimation(2);
                    if(2*Enemy.transform.GetChild(0).GetComponent<Health>().currentHP/EstimDMGpC + FireEstimation(0) > EstimDMGpM*mana){
                        for(i = 0; i < playable.Count; i++){
                            if(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().id == 4){
                                play = i;
                            }
                        }
                        print(playable[play]);
                        gameObject.transform.GetChild(playable[play]).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                    }
                }
                if((play < 0)&&(increase == true)){
                    DMGEstimation(1);
                    if(((Enemy.transform.GetChild(0).GetComponent<Health>().currentHP/EstimDMGpC > EstimDMGpM*mana)&&(Enemy.transform.GetChild(0).GetComponent<Health>().currentHP/EstimDMGpC > FireEstimation(0)))){
                        for(i = 0; i < playable.Count; i++){
                            if(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().id == 2){
                                play = i;
                            }
                            if((play < 0)&&(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().id == 6)){
                                play = i;
                            }
                        }
                        print(playable[play]);
                        gameObject.transform.GetChild(playable[play]).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                    }
                }
                if((play < 0)&&(fire == true)){
                    DMGEstimation(0);
                    if(FireEstimation(60) > EstimDMGpM*mana){
                        for(i = 0; i < playable.Count; i++){
                            if(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().id == 8){
                                play = i;
                            }
                        }
                        print(playable[play]);
                        gameObject.transform.GetChild(playable[play]).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                    }
                }
                if(play < 0){
                    int MAXvalue = 0;
                    for(i = 0; i < playable.Count; i++){
                        switch(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().id){
                            case 1:
                            if(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage > MAXvalue){
                                MAXvalue = gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage;
                                play = i;
                            }
                            break;
                            case 2:
                            if(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage+1 > MAXvalue){
                                MAXvalue = gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage+1;
                                play = i;
                            }
                            break;
                            case 3:
                            if(2*gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage > MAXvalue){
                                MAXvalue = gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage*2;
                                play = i;
                            }
                            break;
                            case 7:
                            if(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage > MAXvalue){
                                MAXvalue = gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage;
                                play = i;
                            }
                            break;
                            case 8:
                            if(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage > MAXvalue){
                                MAXvalue = gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage;
                                play = i;
                            }
                            break;
                        }
                    }
                    print(playable[play]);
                    gameObject.transform.GetChild(playable[play]).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                }
            }
        }else{
            print("End Round");
        }
        /*i = Random.Range(0, playable.Count);
        Events.Cast(gameObject.transform.GetChild(playable[i]).gameObject,  Enemy.transform.parent.GetChild(0).gameObject);
        Events.Discard(gameObject.transform.GetChild(playable[i]).gameObject);*/
    }

    void checkTags(List<int> playable){
        heal = false;
        DMG = false;
        fire = false;
        increase = false;
        EstimDMGpM = 0;
        int DMGCount = 0;
        for(i = 0; i < playable.Count; i++){
            if(gameObject.transform.GetChild(playable[i]).tag.Contains("heal") == true){
                print("hael");
                heal = true;
            }
            if(gameObject.transform.GetChild(playable[i]).tag.Contains("damage") == true){
                print("DMG");
                DMG = true;
                switch(gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().id){
                case 1:
                EstimDMGpM += gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage/gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().mana;
                break;
                case 2:
                EstimDMGpM += (gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage+1)/gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().mana;
                break;
                case 3:
                EstimDMGpM += 2*gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage/gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().mana;
                break;
                case 7:
                EstimDMGpM += gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage/gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().mana;
                break;
                case 8:
                EstimDMGpM += gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().baseDamage/gameObject.transform.GetChild(playable[i]).GetComponent<CardInfo>().mana;
                break;
            }
                DMGCount++;
            }
            if(gameObject.transform.GetChild(playable[i]).tag.Contains("increase") == true){
                print("increase");
                increase = true;
            }
            if(gameObject.transform.GetChild(playable[i]).tag.Contains("fire") == true){
                print("fire");
                fire = true;
            }
        }
        if(DMGCount > 0){
            EstimDMGpM = EstimDMGpM/DMGCount;
        }
    }

    void DMGEstimation(int BDMG){
        EstimDMGpC = 0;
        EstimDMGpT = 0;
        EstimTpC = 0;
        print((float)deck.id1/40);
        for(i = 0; i < DeckScript.DeckCards.Count; i++){
            switch(DeckScript.DeckCards[i].GetComponent<CardInfo>().id){
                case 1:
                EstimDMGpC += (DeckScript.DeckCards[i].GetComponent<CardInfo>().baseDamage+BDMG)*deck.id1/40;
                break;
                case 2:
                EstimDMGpC += (DeckScript.DeckCards[i].GetComponent<CardInfo>().baseDamage+1+BDMG)*deck.id2/40;
                break;
                case 3:
                EstimDMGpC += 2*(DeckScript.DeckCards[i].GetComponent<CardInfo>().baseDamage+BDMG)*deck.id3/40;
                break;
                case 7:
                EstimDMGpC += (DeckScript.DeckCards[i].GetComponent<CardInfo>().baseDamage+BDMG)*deck.id7/40;
                break;
                case 8:
                EstimDMGpC += (DeckScript.DeckCards[i].GetComponent<CardInfo>().baseDamage+BDMG)*deck.id8/40;
                break;
            }
            EstimTpC += DeckScript.DeckCards[i].GetComponent<CardInfo>().time;
        }
        for(i = 0; i < gameObject.transform.childCount; i++){
            switch(gameObject.transform.GetChild(i).GetComponent<CardInfo>().id){
                case 1:
                EstimDMGpC += (gameObject.transform.GetChild(i).GetComponent<CardInfo>().baseDamage+BDMG)*deck.id1/40;
                break;
                case 2:
                EstimDMGpC += (gameObject.transform.GetChild(i).GetComponent<CardInfo>().baseDamage+1+BDMG)*deck.id2/40;
                break;
                case 3:
                EstimDMGpC += 2*(gameObject.transform.GetChild(i).GetComponent<CardInfo>().baseDamage+BDMG)*deck.id3/40;
                break;
                case 7:
                EstimDMGpC += (gameObject.transform.GetChild(i).GetComponent<CardInfo>().baseDamage+BDMG)*deck.id7/40;
                break;
                case 8:
                EstimDMGpC += (gameObject.transform.GetChild(i).GetComponent<CardInfo>().baseDamage+BDMG)*deck.id8/40;
                break;
            }
            EstimTpC +=  gameObject.transform.GetChild(i).GetComponent<CardInfo>().time;
        }
        for(i = 0; i < DiscardScript.DiscardedCards.Count; i++){
            switch(DiscardScript.DiscardedCards[i].GetComponent<CardInfo>().id){
                case 1:
                EstimDMGpC += (DiscardScript.DiscardedCards[i].GetComponent<CardInfo>().baseDamage+BDMG)*deck.id1/40;
                break;
                case 2:
                EstimDMGpC += (DiscardScript.DiscardedCards[i].GetComponent<CardInfo>().baseDamage+1+BDMG)*deck.id2/40;
                break;
                case 3:
                EstimDMGpC += 2*(DiscardScript.DiscardedCards[i].GetComponent<CardInfo>().baseDamage+BDMG)*deck.id3/40;
                break;
                case 7:
                EstimDMGpC += (DiscardScript.DiscardedCards[i].GetComponent<CardInfo>().baseDamage+BDMG)*deck.id7/40;
                break;
                case 8:
                EstimDMGpC += (DiscardScript.DiscardedCards[i].GetComponent<CardInfo>().baseDamage+BDMG)*deck.id8/40;
                break;
            }
            EstimTpC += DiscardScript.DiscardedCards[i].GetComponent<CardInfo>().time;
        }
        EstimTpC = EstimTpC/40;
        EstimDMGpT = EstimDMGpC/EstimTpC;
    }

    int FireEstimation(int setBack){
        if(fire == true){
            int FireCount = Enemy.transform.parent.GetChild(0).GetComponent<TileEffects>().getFireCount();
            float EHP = Enemy.transform.GetChild(0).GetComponent<Health>().currentHP;
            int fireValue = 0;
            if((EHP/EstimDMGpT < 30+setBack)||(FireCount == 3)){
                fireValue = 0;
            }else if((EHP/EstimDMGpT < 60+setBack)||(FireCount == 2)){
                fireValue = 15;
            }else if((EHP/EstimDMGpT < 90+setBack)||(FireCount == 1)){
                fireValue = 30;
            }else{
                fireValue = 45;
            }
            return fireValue;
        }else{
            return 0;
        }
    }

    void StartPlayerTurn(int id){
        if(id == 0){
            unplayed = true;
            waitTime = 1.5f;
        }
    }

    void NextCard(GameObject Card, GameObject pos){
        unplayed = true;
        waitTime = 1.5f;
    }
}
