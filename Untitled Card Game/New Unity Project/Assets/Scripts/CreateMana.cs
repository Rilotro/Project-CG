using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMana : MonoBehaviour
{
    GameObject Main_Canvas;
    public GameObject Mana;

    // Start is called before the first frame update
    void Awake()
    {
        Main_Canvas = GameObject.Find("Main Canvas");
        GameObject newMana = Instantiate(Mana, new Vector3(0, 0, 0), Quaternion.identity);
        newMana.transform.position = new Vector2(151.5f, 0);
        newMana.transform.SetParent(Main_Canvas.transform, false);
        var rect = (RectTransform)newMana.transform;
        rect.sizeDelta = new Vector2(30, 30);
        newMana.transform.SetSiblingIndex(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
