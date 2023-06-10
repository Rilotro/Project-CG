using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public Vector2 position = new Vector2(0, 0);

    void Start(){
        Events.AssignTile(position, gameObject);
    }

    public void TakeDMG(int DMG){
        gameObject.transform.GetChild(0).GetComponent<Health>().DealDMG(DMG, gameObject);
    }
}
