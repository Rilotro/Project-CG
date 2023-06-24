using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    int id, Mpos;
    float time;
    float minX;
    GameObject Target;
    Vector2 AoE;

    public void giveId(int i, int pos, int t, GameObject Target, Vector2 TAoE, Color32 c){
        Events.RoundStartEvent += EventRoundStart;
        Events.CardDisableEvent += RemoveMe;
        id = i;
        Mpos = pos;
        time = (float)t;
        gameObject.transform.GetChild(0).GetComponent<TimeUpdate>().UpdateTime(time);
        this.Target = Target;
        AoE = TAoE;
        gameObject.GetComponent<Image>().color = c;
        minX = (gameObject.transform.parent.GetComponent<RectTransform>().rect.width - gameObject.GetComponent<RectTransform>().rect.width)/2;
        gameObject.transform.localPosition = new Vector3(minX*t/90 - minX, 0, 0);
        

    }

    public void AddTime(int i, int t){
        if((i == id) || (i < 0)){
            time = (float)t;
            if(time < 0){
                time = 0;
            }
            if(time < 180){
                gameObject.transform.localPosition = new Vector3(minX*time/90 - minX, 0, 0);    
            }else{
                gameObject.transform.localPosition = new Vector3(minX, 0, 0);
            }
            gameObject.transform.GetChild(0).GetComponent<TimeUpdate>().UpdateTime(time);
        }
    }

    void EventRoundStart(int id){
        if((id == this.id)&&(time == 0)){
            switch (id){
                case 2:
                Vector2 pos = Target.transform.GetComponent<TileEffects>().pos;
                if((AoE.x < 0)&&(AoE.y < 0)){
                    Events.DealDMG(10, Target);
                }else if(AoE.x == 0){
                    for(int i = (int)pos.y; (i < (int)pos.y+(int)AoE.y)&&(i < 8); i++){
                        Events.DealDMG(10, Target.transform.parent.parent.GetChild((int)pos.x*8+i).GetChild(0).gameObject);
                    }
                }else if((AoE.x != 0)&&(AoE.y != 0)){
                    for(int i = (int)pos.x - ((int)AoE.x-1)/2; (i <= (int)pos.x + ((int)AoE.x-1)/2)&&(i < 4); i++){
                        if(i < 0){
                            i = 0;
                        }
                        for(int j = (int)pos.y - ((int)AoE.y-1)/2; (j <= (int)pos.y + ((int)AoE.y-1)/2)&&(j < 8); j++){
                            if(j < 0){
                                j = 0;
                            }
                            Events.DealDMG(10, Target.transform.parent.parent.GetChild((int)i*8+j).GetChild(0).gameObject);
                        }
                    }
                }
                Events.RoundEnd(id);
                break;
                case 3:
                Events.giveBDMG(5, 0, Target);
                gameObject.transform.parent.GetComponent<RoundSystem>().addEventsTime(Mpos, 60);
                Events.RoundEnd(id);
                break;
            }
        }
    }

    void RemoveMe(GameObject Card){
        if(Target == Card){
            gameObject.transform.parent.GetComponent<RoundSystem>().removeMarker(Mpos);
            Destroy(gameObject);
        }
    }

    void OnDisable(){
        Events.RoundStartEvent -= EventRoundStart;
        Events.CardDisableEvent -= RemoveMe;
    }
}
