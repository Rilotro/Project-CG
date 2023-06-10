using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ManaPoints : MonoBehaviour
{
    int startMana = 0;
    int currentMana;

    void Awake()
    {
        Events.UpdateManaEvent += updateMana;
        currentMana = startMana;
        gameObject.GetComponentsInChildren<Text>()[0].text = startMana.ToString();
    }

    public void updateMana(int manaSpent){
        currentMana = currentMana - manaSpent;
        gameObject.GetComponentsInChildren<Text>()[0].text = currentMana.ToString();
        Events.GiveMana(currentMana);
    }
    public int getMana(){
        return currentMana;
    }

    void OnDisable(){
        Events.UpdateManaEvent -= updateMana;
    }


    // Update is called once per frame
}
