using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HPUpdate : MonoBehaviour
{
    public void updateHP(float currentHP, float maxHP)
    {
        gameObject.GetComponent<Text>().text = string.Format("{0} / {1}", currentHP, maxHP);
    }
}
