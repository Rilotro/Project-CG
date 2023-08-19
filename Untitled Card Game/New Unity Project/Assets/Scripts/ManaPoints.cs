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
        Events.ReloadEvent += End;
        currentMana = startMana;
        gameObject.GetComponentsInChildren<Text>()[0].text = startMana.ToString();
    }

    public void updateMana(int manaSpent){
        currentMana = currentMana - manaSpent;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = currentMana.ToString();
        Events.GiveMana(currentMana);
    }
    public int getMana(){
        return currentMana;
    }

    void End(){
        currentMana = 0;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = currentMana.ToString();
    }

    void OnDisable(){
        Events.UpdateManaEvent -= updateMana;
        Events.ReloadEvent -= End;
    }


    // Update is called once per frame
}
