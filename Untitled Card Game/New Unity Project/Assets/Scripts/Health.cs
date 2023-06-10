using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Health : MonoBehaviour
{
    public float currentHP, maxHP;
    GameObject Target;

    public UnityDMGEvent UpdateHealthBar;

    

    public void DealDMG(int DMG, GameObject Target)
    {
        if(gameObject.transform.parent.gameObject == Target){
            if ((currentHP - DMG >= 0)&&(currentHP - DMG <= maxHP)){
                currentHP -= DMG;
                UpdateHealthBar.Invoke(currentHP, maxHP);
            }else if(currentHP - DMG > maxHP){
                currentHP = maxHP;
                UpdateHealthBar.Invoke(currentHP, maxHP);
            }else if(currentHP != 0){
                currentHP = 0;
                UpdateHealthBar.Invoke(currentHP, maxHP);
            }
        }

    }

    void Awake()
    {
        maxHP = 100;
        currentHP = maxHP;;
    }
    void Start(){
        Events.DealDMGEvent += DealDMG;
        UpdateHealthBar.Invoke(currentHP, maxHP);
    }

    void OnDisable(){
        Events.DealDMGEvent -= DealDMG;
    }
}

[System.Serializable]
public class UnityDMGEvent : UnityEvent<float, float> { }
