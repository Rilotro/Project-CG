using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragnDrop : MonoBehaviour
{
    int mana = 0;
    public static GameObject active;

    void Awake()
    {
        Events.GetMana += GetMana;
        Events.CastCardEvent += Activated;
    }

    void GetMana(int newMana){
        mana = newMana;
    }

    public void Activated(GameObject pos)
    {
        if(gameObject == active)
        {
            active = null;
            Events.Cast(gameObject, pos);
            Events.Discard(gameObject);
        }
    }

    public void onClick(BaseEventData data){
        PointerEventData Pdata = (PointerEventData)data;
        if((Pdata.pointerId == -1)&&(mana >= gameObject.GetComponent<CardInfo>().getCost())&&(gameObject.transform.parent.gameObject.name == "PlayerHand")){
            active = gameObject;
            if(TileEffects.Player != null){
                TileEffects.Player = null;
            }
            Events.giveSize(gameObject.GetComponent<CardInfo>().AoE);
            Events.Recolor(new Color32(0, 255, 0, 50));
        }
    }

    void OnDisable(){
        Events.GetMana -= GetMana;
        Events.CastCardEvent -= Activated;
    }
}
