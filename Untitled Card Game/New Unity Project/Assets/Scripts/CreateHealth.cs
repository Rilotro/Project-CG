using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Health;
    public GameObject newHealth;
    public int HP = 40;
    public GameObject Main_Canvas;

    public int getHP(){
        return HP;
    }

    void Awake()
    {
        Main_Canvas = GameObject.Find("Main Canvas");
        newHealth = Instantiate(Health, new Vector3(0, 0, 0), Quaternion.identity);
        newHealth.transform.SetParent(Main_Canvas.transform, false);
        var rect = (RectTransform)newHealth.transform;
        rect.sizeDelta = new Vector2(115, 25);
        newHealth.transform.position = new Vector2(413, 275);
        newHealth.transform.SetSiblingIndex(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
