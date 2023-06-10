using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Events : MonoBehaviour
{
    public static event Action<int> DrawCardsEvent;//DrawCards - DrawC;
    public static event Action<int, GameObject> DealDMGEvent;//Health - DealDMG TileEffects - EffectDMG;
    public static event Action<GameObject, GameObject> CastEvent;//DrawCards - cardDestroyed, EffectDataBase - Cast;
    public static event Action<int, int> AddUnitTimeEvent;//RoundSystem - addUnitTime;
    public static event Action<int> UpdateManaEvent;//ManaPoints - updateMana;
    public static event Action<int, int, GameObject, Vector2, Color32> AddMarkerEvent;//RoundSystem - addEvent;
    public static event Action<int> RoundStartEvent;//EffectDataBase - PlayerRoundStart, EnemyAI - RoundStart, Marker - EventRoundStart, TileEffects - RoundStartRevert;
    public static event Action<int> RoundEndEvent;//CardInfo - DestroyMe, DrawCards - DestroyAllCards, RoundSystem - EventRoundEnd;
    public static event Action<int> GetMana;//DragnDrop - GetMana;
    public static event Action ResumeTurnSystem;//RoundSystem - TurnSystem;
    public static event Action<Vector2, GameObject> AssignTileEvent;//DealDamage - AssignParent;
    public static event Action<GameObject> CastCardEvent;//DragnDrop - Activated;
    public static event Action Relocate;//PlayerRelocation - BeginRelocation;
    public static event Action<Color32> RecolorTiles;//TileEffects - Recolored;
    public static event Action<Vector2> giveTilesAoE;//TileEffects - getAoE;

    static float wait = -1;
    static bool resumed = true;

    public static void AssignTile(Vector2 pos, GameObject child){//UnitScript - Start;
        AssignTileEvent?.Invoke(pos, child);
    } 

    public static void GiveMana(int mana){//ManaPoints - updateMana;
        GetMana?.Invoke(mana);
    }
    
    public static void Draw(int n){//EffectDataBase - PlayerRoundStart - Effects;
        DrawCardsEvent?.Invoke(n);
    }

    public static void AddMana(int mana){//EffectDataBase - PlayerRoundStart - Effects;
        UpdateManaEvent?.Invoke(mana*(-1));
    }

    public static void Cast(GameObject Card, GameObject Target){//DragnDrop - Activated;
        AddUnitTimeEvent?.Invoke(0, Card.transform.GetComponent<CardInfo>().getTime());
        UpdateManaEvent?.Invoke(Card.transform.GetComponent<CardInfo>().getCost());
        CastEvent?.Invoke(Card, Target);
    }

    public static void AddUnitTime(int id, int time_cost){//EnemyAI - RoundStart;
        AddUnitTimeEvent?.Invoke(id, time_cost);
    }

    public static void DealDMG(int DMG, GameObject Target){//EffectDataBase - Effects, EnemyAI - RoundStart, Marker - EventRoundStart;
        DealDMGEvent?.Invoke(DMG, Target);
    }

    public static void addEvent(int id, int time, GameObject Target, Vector2 TAoE, Color32 c){//EffectDataBase - Effects;
        AddMarkerEvent?.Invoke(id, time, Target, TAoE, c);
    }

    public static void RoundStart(int id){//RoundSystem - Event(class);
        RoundStartEvent?.Invoke(id);
    }

    public static void activateCard(GameObject pos){//TileEffects - onClick;
        CastCardEvent?.Invoke(pos);
    }

    public static void ActivateRelocation(){//PlayerBeginRelocation - OnClick;
        Relocate?.Invoke();
    }

    public static void Recolor(Color32 c){//PlayerRelocation - BeginRelocation, GragnDrop - onClick;
        RecolorTiles?.Invoke(c);
    }

    public static void giveSize(Vector2 size){//DragnDrop - onClick, TileEffects - onClick;
        giveTilesAoE?.Invoke(size);
    }

    public static void RoundEnd(int id){//EndRound - EndonClick, EnemyAI - RoundStart, Marker - EventRoundStart;
        RoundEndEvent?.Invoke(id);
        resumed = false;
        wait = 2f;
    }

    void Update(){
        if(wait > 0){
            wait -= Time.deltaTime;
        }else if(resumed == false){
            resumed = true;
            Resume();
        }
    }

    void Resume(){
        ResumeTurnSystem?.Invoke();
    }
}
