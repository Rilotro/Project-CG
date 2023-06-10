using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpdate : MonoBehaviour
{

    public void UpdateTime(float time){
        gameObject.transform.GetComponent<Text>().text =  string.Format("{0}", time);
    }
}
