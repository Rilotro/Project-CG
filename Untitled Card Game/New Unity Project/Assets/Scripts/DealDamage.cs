using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealDamage : MonoBehaviour
{
    
    GameObject[][] Tiles = new GameObject[11][];
    int i, j;// count = 0;
    Vector2 firstPos;
    //float waitTime;
    public GameObject Tile;

    void Awake(){
        Events.AssignTileEvent += AssignParent;
        firstPos = new Vector2((-1)*(gameObject.GetComponent<RectTransform>().rect.width - Tile.GetComponent<RectTransform>().rect.width)/2, (gameObject.GetComponent<RectTransform>().rect.height - Tile.GetComponent<RectTransform>().rect.height)/2);
        Tiles[0] = new GameObject[11];
        Tiles[1] = new GameObject[11];
        Tiles[2] = new GameObject[11];
        Tiles[3] = new GameObject[11];
        Tiles[4] = new GameObject[11];
        Tiles[5] = new GameObject[11];
        Tiles[6] = new GameObject[11];
        for(i = 0; i < 7; i++){
            for(j = 0; j < 11; j++){
                Tiles[i][j] = Instantiate(Tile, new Vector3(60, 87, 0), Quaternion.identity);
                Tiles[i][j].transform.SetParent(gameObject.transform, false);
                Tiles[i][j].transform.localPosition = new Vector2(firstPos.x + 80*j, firstPos.y - 80*i);
                Tiles[i][j].name = string.Format("Tile{0}-{1}", i, j);
                Tiles[i][j].transform.GetChild(0).GetComponent<TileEffects>().pos = new Vector2(i, j);
            }
        }
        i--;
        j--;
        //waitTime = 3f;
    }

    public GameObject getTile(Vector2 pos){
        return Tiles[(int)pos.x][(int)pos.y];
    }

    public void Deal(int DMG, Vector2 position){
        //Tiles[position.x][position.y].GetComponent<>().DealDMG(DMG);
    }

    void AssignParent(Vector2 pos, GameObject child){
        child.transform.SetParent(Tiles[(int)pos.x][(int)pos.y].transform, false);
        child.transform.localPosition = new Vector2(0, 0);
    }

    void OnDisable(){
        Events.AssignTileEvent -= AssignParent;
    }

    /*void Update(){
        if(waitTime > 0){
            waitTime -= Time.deltaTime;
        }else{
            Tiles[i][j].GetComponent<Image>().color = new Color32(0, 255, 0, 100);
            count++;
            if(j > 0){
                j--;
                waitTime = (3 - 0.1f*count);
            }else if(i > 0){
                i--;
                j = 7;
                waitTime = (3 - 0.1f*count);
            }
        }
    }*/
}
