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
    float waitTime = 0f;
    public GameObject GO;
    public UnityDMGEvent UpdateHealthBar;

    

    public void DealDMG(int DMG, GameObject Target)
    {
        if(gameObject.transform.parent.gameObject == Target){
            if ((currentHP - DMG > 0)&&(currentHP - DMG <= maxHP)){
                currentHP -= DMG;
                UpdateHealthBar.Invoke(currentHP, maxHP);
            }else if(currentHP - DMG > maxHP){
                currentHP = maxHP;
                UpdateHealthBar.Invoke(currentHP, maxHP);
            }else if(currentHP != 0){
                currentHP = 0;
                UpdateHealthBar.Invoke(currentHP, maxHP);
                /*GameObject Overlay = Instantiate(GO, new Vector3(0, 0, 0), Quaternion.identity);
                Overlay.transform.SetParent(gameObject.transform.parent.parent.parent.parent, false);
                if(gameObject.transform.parent.name.Contains("Player") == true){
                    Overlay.transform.GetComponent<GOScript>().GameOver(0);
                }else{
                    Overlay.transform.GetComponent<GOScript>().GameOver(1);
                }*/
                //waitTime = 2f;
                Events.Reload();
            }
        }

    }

    void Awake()
    {
        maxHP = 100;
        currentHP = maxHP;
        print(gameObject.GetComponent<RectTransform>().rect.width);
    }
    void Start(){
        //Events.DealDMGEvent += DealDMG;
        UpdateHealthBar.Invoke(currentHP, maxHP);
    }

    /*void Update(){
        if(waitTime > 0){
            waitTime -= Time.deltaTime;
        }else if(currentHP == 0){
            //Destroy(gameObject.transform.parent.gameObject);
            //Events.Reload();
        }
    }*/
}

[System.Serializable]
public class UnityDMGEvent : UnityEvent<float, float> { }
