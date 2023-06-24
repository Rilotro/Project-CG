using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RoundSystem : MonoBehaviour
{
    public int Count = 0;
    public int EventCount = 0;
    int i;
    GameObject PlayerHand;
    GameObject EnemyUnit;
    public GameObject EventPrefab;

    class Event{
        int id, time;
        GameObject Icon;
        public Event(int i, int t, GameObject nI){
            id = i;
            time = t;
            Icon = nI;
        }
        public void addTime(int t){
            time += t;
            if(time < 0){
                time = 0;
            }
            Icon.GetComponent<Marker>().AddTime(id, time);
        }
        public int getTime(){
            return time;
        }
        public int getId(){
            return id;
        }
        public void RoundStart(){
            Events.RoundStart(id);
        }
    }

    List<Event> Markers = new List<Event>();
    int Closest;

    public void addEvent(int id, int t, GameObject Target, Vector2 size, Color32 c){
        for(i = 0; i < Markers.Count; i++){
            if(Markers[i].getTime() == t){
                t++;
                i = -1;
            }
        }
        GameObject newEvent = Instantiate(EventPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Markers.Add(new Event(id, t, newEvent));
        int pos = Markers.Count - 1;
        newEvent.transform.SetParent(gameObject.transform, false);
        newEvent.GetComponent<Marker>().giveId(id, pos, t, Target, size, c);
    }

    void Start(){
        Events.ResumeTurnSystem += TurnSystem;
        Events.RoundEndEvent += EventRoundEnd;
        Events.AddUnitTimeEvent += addUnitTime;
        Events.AddMarkerEvent += addEvent;
        addEvent(0, 0, null, new Vector2(-1, -1), new Color32(0, 255, 0, 255));//Player
        addEvent(1, 1, null, new Vector2(-1, -1), new Color32(255, 0, 0, 255));//Enemy
        Markers[0].RoundStart();
    }

    public void TurnSystem(){
        Closest = 0;
        for(int i = 1; i < Markers.Count; i++){
            if(Markers[Closest].getTime() > Markers[i].getTime()){
                Closest = i;
            }
        }
        for(int i = 0; i < Markers.Count; i++){
            if(i != Closest){
                Markers[i].addTime(Markers[Closest].getTime() * (-1));
            }
        }
        Markers[Closest].addTime(Markers[Closest].getTime() * (-1));
        Markers[Closest].RoundStart();
    }
    
    public void addUnitTime(int id, int t){
        bool found = false;
        int j = -1;
        for(int i = 0; (i < Markers.Count) && (found == false); i++){
            if(Markers[i].getId() == id){
                found = true;
                Markers[i].addTime(t);
                j = i;
            }
        }
        if(j >= 0){
            print(Markers[j].getTime());
            for(int i = 0; i < Markers.Count; i++){
                if((Markers[i].getTime() == Markers[j].getTime())&&(i != j)){
                    print(i);
                    Markers[j].addTime(1);
                    i = -1;
                }
            }
        }
    }

    public void addEventsTime(int pos, int t){
        Markers[pos].addTime(t);
        for(int i = 0; i < Markers.Count; i++){
                if((Markers[i].getTime() == Markers[pos].getTime())&&(i != pos)){
                    print(i);
                    Markers[pos].addTime(1);
                    i = -1;
                }
            }
    }

    public void EventRoundEnd(int id){
        bool found = false;
        for(int i = 2; (i < Markers.Count) && (found == false); i++){
            if((Markers[i].getId() == id)&&(Markers[i].getTime() == 0)){
                found = true;
                Markers.Remove(Markers[i]);
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }

    public void removeMarker(int pos){
        Markers.Remove(Markers[pos]);
    }

    void OnDisable(){
        Events.ResumeTurnSystem -= TurnSystem;
        Events.RoundEndEvent -= EventRoundEnd;
        Events.AddUnitTimeEvent -= addUnitTime;
        Events.AddMarkerEvent -= addEvent;
    }
}
