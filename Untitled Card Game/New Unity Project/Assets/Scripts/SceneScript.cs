using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    public GameObject CardE;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Event;

    GameObject DeckOverlay;
    GameObject DiscardOverlay;
    GameObject PlayerHand;
    GameObject BackGround;
    GameObject EventBar;

    GameObject PlayerUnit;
    GameObject EnemyUnit;

    float waitTime = 0f;
    bool Reloaded = false;

    [Serializable]
    public class Card{
        public int id, dmg, mana, time;
        public Card(int Id, int DMG, int Mana, int T){
            id = Id;
            dmg = DMG;
            mana = Mana;
            time = T;
        }
    }

    [Serializable]
    public class Unit{
        public int id;
        public float health, maxHP;
        public Vector2 pos;
        public Unit(int Id, float HP, float MHP, Vector2 Pos){
            id = Id;
            health = HP;
            maxHP = MHP;
            pos = Pos;
        }
    }

    [Serializable]
    public class Fire{
        public int counter;
        public Vector2 pos;
        public Fire(int c, Vector2 Pos){
            counter = c;
            pos = Pos;
        }
    }

    [Serializable]
    public class Marcher{
        public int id, time;
        public Vector2 target;
        public Color32 color;
        public Vector2 size;
        public Marcher(int Id, int t, Vector2 Target, Vector2 Size, Color32 c){
            id = Id;
            time = t;
            target = Target;
            color = c;
            size = Size;
        }
    }

    [Serializable]
    public class Scene{
        public List<Card> Deck = new List<Card> ();
        public List<Card> Hand = new List<Card> ();
        public List<Card> Discard = new List<Card> ();

        public List<Unit> Units = new List<Unit> ();

        public List<Fire> Fires = new List<Fire>();

        public List<Marcher> Markers = new List<Marcher> ();
    }

    [Serializable]
    public class Action{
        public int id, damage, time, mana;
        public Vector2 AoE, Tpos;
        public Scene currentScene;

        public Action(){
            id = -1;
            damage = 0;
            time = 0;
            mana = 0;
            Tpos = new Vector2(-1, -1);
            AoE = new Vector2(-1, -1);
            currentScene = new Scene();
        }

        /*public void MarkerAction(int Id, int DMG, Vector2 Tile, Vector2 TAoE, Scene scene){
            id = Id;
            damage = DMG;
            Tpos = Tile;
            AoE = TAoE;
            currentScene = scene;
        }

        public void FireAction(Scene scene){
            id = 4;
            damage = 15;
            currentScene = scene;
        }

        public void EnemyAction(int DMG, int Time, Vector2 Tile, Vector2 TAoE, Scene scene){
            id = 1;
            damage = DMG;
            time = Time;
            Tpos = Tile;
            AoE = TAoE;
            currentScene = scene;
        }*/

        public void CardAction(int Id, int DMG, int Time, int Mana, Vector2 Tile, Vector2 TAoE, Scene scene){
            id = Id;
            damage = DMG;
            time = Time;
            mana = Mana;
            Tpos = Tile;
            AoE = TAoE;
            currentScene = scene;
        }
    }

    public List<Action> History = new List<Action>();

    public void Backup(Scene savedScene){
        int i;
        GameObject tempCard;
        for(i = 0; i < savedScene.Deck.Count; i++){
            tempCard = Instantiate(CardE, new Vector3(-500, 0, 0), Quaternion.identity);
            tempCard.transform.SetParent(DeckOverlay.transform, false);
            DeckScript.DeckCards.Add(tempCard);
            tempCard.GetComponent<CardInfo>().Give(savedScene.Deck[i].id);
            tempCard.GetComponent<CardInfo>().baseDamage = savedScene.Deck[i].dmg;
            tempCard.GetComponent<CardInfo>().mana = savedScene.Deck[i].mana;
            tempCard.GetComponent<CardInfo>().updateDesc(0, -1, null);
        }
        for(i = 0; i < savedScene.Hand.Count; i++){
            tempCard = Instantiate(CardE, new Vector3(-500, 0, 0), Quaternion.identity);
            tempCard.transform.SetParent(PlayerHand.transform, false);
            tempCard.GetComponent<CardInfo>().Give(savedScene.Hand[i].id);
            tempCard.GetComponent<CardInfo>().baseDamage = savedScene.Hand[i].dmg;
            tempCard.GetComponent<CardInfo>().mana = savedScene.Hand[i].mana;
            tempCard.GetComponent<CardInfo>().updateDesc(0, -1, null);
        }
        for(i = 0; i < savedScene.Discard.Count; i++){
            tempCard = Instantiate(CardE, new Vector3(-500, 0, 0), Quaternion.identity);
            tempCard.transform.SetParent(DiscardOverlay.transform, false);
            DiscardScript.DiscardedCards.Add(tempCard);
            tempCard.GetComponent<CardInfo>().Give(savedScene.Discard[i].id);
            tempCard.GetComponent<CardInfo>().baseDamage = savedScene.Discard[i].dmg;
            tempCard.GetComponent<CardInfo>().mana = savedScene.Discard[i].mana;
            tempCard.GetComponent<CardInfo>().updateDesc(0, -1, null);
        }

        GameObject unit = Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity);
        unit.transform.SetParent(BackGround.transform.GetChild((int)(savedScene.Units[0].pos.x*11+savedScene.Units[0].pos.y)), false);
        unit.transform.localPosition = new Vector2(0, 0);
        unit.transform.GetChild(0).GetComponent<Health>().currentHP = savedScene.Units[0].health;
        unit.transform.GetChild(0).GetComponent<Health>().DealDMG(0, unit);
        PlayerUnit = unit;

        unit = Instantiate(Enemy, new Vector3(0, 0, 0), Quaternion.identity);
        unit.transform.SetParent(BackGround.transform.GetChild((int)(savedScene.Units[1].pos.x*11+savedScene.Units[1].pos.y)), false);
        unit.transform.localPosition = new Vector2(0, 0);
        unit.transform.GetChild(0).GetComponent<Health>().currentHP = savedScene.Units[1].health;
        unit.transform.GetChild(0).GetComponent<Health>().DealDMG(0, unit);
        unit.GetComponent<EnemyScript>().safetyMeassure(new Vector2(0, 0), Player);
        EnemyUnit = unit;

        for(i = 0; i < savedScene.Fires.Count; i++){
            BackGround.transform.GetChild((int)(savedScene.Fires[i].pos.x*11+savedScene.Fires[i].pos.y)).GetChild(0).GetComponent<TileEffects>().GetIgnited();
            BackGround.transform.GetChild((int)(savedScene.Fires[i].pos.x*11+savedScene.Fires[i].pos.y)).GetChild(0).GetComponent<TileEffects>().setCount(savedScene.Fires[i].counter);
        }

        GameObject Target;
        for(i = 0; i < savedScene.Markers.Count; i++){
            Target = BackGround.transform.GetChild((int)savedScene.Markers[i].target.x*11+(int)savedScene.Markers[i].target.y).GetChild(0).gameObject;
            Events.addEvent(savedScene.Markers[i].id, savedScene.Markers[i].time, Target, savedScene.Markers[i].size, savedScene.Markers[i].color);
        }

        Events.RoundStart(0);
    }

    void Awake(){
        Events.ReloadEvent += PostponedReload;
        //Events.RoundStartEvent += StoreAction;
        Events.CastEvent += StoreCAction;
    }

    void Start(){
        int i, j;
        for(i = 0; i < 7; i++){
            for(j = 0; j < 11; j++){
                if(BackGround.transform.GetChild(i*11+j).childCount > 1){
                    if(BackGround.transform.GetChild(i*11+j).GetChild(1).name.Contains("Player")){
                        PlayerUnit = BackGround.transform.GetChild(i*11+j).GetChild(1).gameObject;
                    }else if(BackGround.transform.GetChild(i*11+j).GetChild(1).name.Contains("Enemy")){
                        EnemyUnit = BackGround.transform.GetChild(i*11+j).GetChild(1).gameObject;
                    }
                }
            }
        }
        /*Scene startScene = new Scene();
        int j = Random.Range(1, 41);
        for(i = 0; i < j; i++){
            startScene.Deck.Add(new Card(Random.Range(1, 9), Random.Range(1, 15), Random.Range(1, 3)));
        }
        j = Random.Range(1, 5);
        for(i = 0; i < j; i++){
            startScene.Hand.Add(new Card(Random.Range(1, 9), Random.Range(1, 15), Random.Range(1, 3)));
        }
        j = Random.Range(1, 21);
        for(i = 0; i < j; i++){
            startScene.Discard.Add(new Card(Random.Range(1, 9), Random.Range(1, 15), Random.Range(1, 3)));
        }

        startScene.Units.Add(new Unit(0, Random.Range(1, 101), 100, new Vector2(Random.Range(0, 7), Random.Range(0, 11))));
        Vector2 Epos = new Vector2(Random.Range(0, 7), Random.Range(0, 11));
        while(Epos == startScene.Units[0].pos){
            Epos = new Vector2(Random.Range(0, 7), Random.Range(0, 11));
        }
        startScene.Units.Add(new Unit(1, Random.Range(1, 101), 100, Epos));

        TileEffects.FireExists = true;
        startScene.Fires.Add(new Fire(Random.Range(1, 4), Epos));
        Epos.x += 1;
        startScene.Fires.Add(new Fire(Random.Range(1, 4), Epos));
        Epos.y += 1;
        startScene.Fires.Add(new Fire(Random.Range(1, 4), Epos));

        startScene.Markers.Add(new Marcher(0, Random.Range(0, 61), null, new Vector2(-1, -1), new Color32(0, 255, 0, 255)));
        startScene.Markers.Add(new Marcher(1, Random.Range(0, 61), null, new Vector2(-1, -1), new Color32(255, 0, 0, 255)));
        startScene.Markers.Add(new Marcher(4, Random.Range(0, 31), BackGround.transform.GetChild(0).GetChild(0).gameObject, new Vector2(0, 0), new Color32(0, 0, 0, 0)));

        Backup(startScene);*/
    }

    void PostponedReload(){
        waitTime = 3f;
        Reloaded = true;
    }

    void Update(){
        if(waitTime > 0){
            waitTime -= Time.deltaTime;
        }else if(Reloaded == true){
            Reloaded = false;
            Reload();
        }
    }

    void Reload(){
        int i;
        string json = "[";
        json += JsonUtility.ToJson(History[0]);
        for(i = 1; i < History.Count; i++){
            json += "," + JsonUtility.ToJson(History[i]);
        }
        json += "]";
        print(json);
        File.WriteAllText(@"d:\Test.json", json);

        Scene startScene = new Scene();

        startScene.Units.Add(new Unit(0, 100, 100, new Vector2(1, 2)));
        startScene.Units.Add(new Unit(1, 100, 100, new Vector2(2, 5)));

        startScene.Markers.Add(new Marcher(0, 0, new Vector2(-1, -1), new Vector2(-1, -1), new Color32(0, 255, 0, 255)));
        startScene.Markers.Add(new Marcher(1, 1, new Vector2(-1, -1), new Vector2(-1, -1), new Color32(255, 0, 0, 255)));
        
        int id;
        for(i = 0; i < 40; i++){
            id = UnityEngine.Random.Range(1, 9);
            startScene.Deck.Add(new Card(id, PlayerHand.GetComponent<CardDataBase>().cardList[id].giveDMG(), PlayerHand.GetComponent<CardDataBase>().cardList[id].giveMana(), PlayerHand.GetComponent<CardDataBase>().cardList[id].giveTime()));
        }

        Backup(startScene);
    }

    /*void StoreAction(int id){
        if(id == 1){
            Action EAction = new Action();
            if(EnemyUnit.GetComponent<EnemyScript>().getIntent() < 70){
                EAction.EnemyAction(10, 60, EnemyUnit.GetComponent<EnemyScript>().PlayerTile.transform.GetChild(0).GetComponent<TileEffects>().pos, new Vector2(3, 3), StoreScene());
            }else{
                EAction.EnemyAction(15, 90, EnemyUnit.GetComponent<EnemyScript>().PlayerTile.transform.GetChild(0).GetComponent<TileEffects>().pos, new Vector2(5, 5), StoreScene());
            }
            History.Add(EAction);
        }else if(id == 4){
            Action FAction = new Action();
            FAction.FireAction(StoreScene());
            History.Add(FAction);
        }else if(id == 2){
            int i;
            int eventCount = 0;
            Action MAction = new Action();
            Vector2 Tile = new Vector2(-1, -1);
            for(i = 0; i < History.Count; i++){
                if((History[i].mana == 0)&&(History[i].id == 2)){
                    eventCount++;
                }
            }
            for(i = 0; i < History.Count; i++){
                if((History[i].mana > 0)&&(History[i].id == 8)){
                    if(eventCount > 0){
                        eventCount--;
                    }else{
                        Tile = History[i].Tpos;
                    }
                }
                
            }
            MAction.MarkerAction(2, 10, Tile, new Vector2(3, 3), StoreScene());
            History.Add(MAction);
        }
    }*/

    void StoreCAction(GameObject Card, GameObject Tile){
        Action CAction = new Action();
        CAction.CardAction(Card.transform.GetComponent<CardInfo>().id, Card.transform.GetComponent<CardInfo>().baseDamage, Card.transform.GetComponent<CardInfo>().time, Card.transform.GetComponent<CardInfo>().mana, Tile.GetComponent<TileEffects>().pos, Card.transform.GetComponent<CardInfo>().AoE, StoreScene());
        History.Add(CAction);
        string json = JsonUtility.ToJson(History[History.Count-1]);
        print(json);
    }

    Scene StoreScene(){
        int i, j;
        Scene currentScene = new Scene();

        GameObject tempCard;
        for(i = 0; i < DeckScript.DeckCards.Count; i++){
            tempCard = DeckScript.DeckCards[i];
            currentScene.Deck.Add(new Card(tempCard.GetComponent<CardInfo>().id, tempCard.GetComponent<CardInfo>().baseDamage, tempCard.GetComponent<CardInfo>().mana, tempCard.GetComponent<CardInfo>().time));
        }

        for(i = 0; i < PlayerHand.transform.childCount; i++){
            tempCard = PlayerHand.transform.GetChild(i).gameObject;
            currentScene.Hand.Add(new Card(tempCard.GetComponent<CardInfo>().id, tempCard.GetComponent<CardInfo>().baseDamage, tempCard.GetComponent<CardInfo>().mana, tempCard.GetComponent<CardInfo>().time));
        }

        for(i = 0; i < DiscardScript.DiscardedCards.Count; i++){
            tempCard = DiscardScript.DiscardedCards[i];
            currentScene.Discard.Add(new Card(tempCard.GetComponent<CardInfo>().id, tempCard.GetComponent<CardInfo>().baseDamage, tempCard.GetComponent<CardInfo>().mana, tempCard.GetComponent<CardInfo>().time));
        }

        currentScene.Units.Add(new Unit(0, Player.transform.GetChild(0).GetComponent<Health>().currentHP, 100, Player.GetComponent<UnitScript>().position));
        currentScene.Units.Add(new Unit(1, Enemy.transform.GetChild(0).GetComponent<Health>().currentHP, 100, Enemy.GetComponent<EnemyScript>().position));

        for(i = 0; i < 7; i++){
            for(j = 0; j < 11; j++){
                if(BackGround.transform.GetChild(i*11+j).GetChild(0).GetComponent<TileEffects>().getFireCount() > 0){
                    currentScene.Fires.Add(new Fire(BackGround.transform.GetChild(i*11+j).GetChild(0).GetComponent<TileEffects>().getFireCount(), new Vector2(i, j)));
                }
            }
        }

        GameObject tempMarker;
        for(i = 0; i < EventBar.transform.childCount; i++){
            tempMarker = EventBar.transform.GetChild(i).gameObject;
            if(tempMarker.GetComponent<Marker>().id > 1){
                currentScene.Markers.Add(new Marcher(tempMarker.GetComponent<Marker>().id, (int)tempMarker.GetComponent<Marker>().time, tempMarker.GetComponent<Marker>().Target.GetComponent<TileEffects>().pos, tempMarker.GetComponent<Marker>().AoE, tempMarker.GetComponent<Image>().color));
            }else{
                currentScene.Markers.Add(new Marcher(tempMarker.GetComponent<Marker>().id, (int)tempMarker.GetComponent<Marker>().time, new Vector2(-1, -1), tempMarker.GetComponent<Marker>().AoE, tempMarker.GetComponent<Image>().color));
            }
        }

        return currentScene;
    }

    public void givePlayerHand(GameObject given){
        PlayerHand = given;
    }

    public void giveDeckOverlay(GameObject given){
        DeckOverlay = given;
    }

    public void giveDiscardOverlay(GameObject given){
        DiscardOverlay = given;
    }

    public void giveEventBar(GameObject given){
        EventBar = given;
    }

    public void giveBackGround(GameObject given){
        BackGround = given;
    }

    void OnDisable(){
        Events.ReloadEvent -= PostponedReload;
        //Events.RoundStartEvent -= StoreAction;
        Events.CastEvent -= StoreCAction;
    }
}
